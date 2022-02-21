using UnityEngine;

public class SubmarineController : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField]
    float moveHorizontalForce = 100f; // Amount of force added to move the player

    [SerializeField]
    float moveVerticalForce = 100f; // Amount of force added to move the player

    [SerializeField]
    float maxHorizontalSpeed = 2f;

    [SerializeField]
    float maxVerticalSpeed = 2f;

    [Header("Oxygen Settings")]
    [SerializeField]
    float increaseOxygenSpeed = 0.25f;

    [SerializeField]
    float decreaseOxygenSpeed = 0.1f;

    [SerializeField]
    float oxygenDecreaseRate = 1.0f;

    [Header("Animation Settings")]
    [SerializeField]
    float moveAnimationSpeed = 0.6f;

    [SerializeField]
    float baseAnimationSpeed = 0.2f;

    [SerializeField]
    bool facingRight;   // For determining which way the player is currently facing.

    [Header("References Settings")]
    [SerializeField]
    Animator UIAnimator;

    [SerializeField]
    EventManager EventManager;

    [SerializeField]
    ProgressBar OxygenBar;

    Rigidbody2D rigidBody;

    Animator animator;

    Vector2 direction;

    int health = 3;

    bool hitTaken = false;

    bool OxygenOut = false;

    // Touch & Mouse control
    Vector2 LastClickedPosition;

    bool isMoving = false;

    bool isTouchInputAvailable = false;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (rigidBody == null)
            Debug.LogError($"RigidBody reference missing on {gameObject.name}!", this);

        if (animator == null)
            Debug.LogError($"Animator reference missing on {gameObject.name}!", this);

        if (EventManager == null)
            Debug.LogError($"Event manager reference missing on {gameObject.name}!", this);

        if (OxygenBar == null)
            Debug.LogError($"OxygenBar reference missing on {gameObject.name}!", this);

        isTouchInputAvailable = Input.touchSupported;
    }

    void Start()
    {
        if (UIAnimator == null)
        {
            Debug.LogError("UI Animator must be set!", UIAnimator);
        }

        InvokeRepeating("DecreaseOxygen", 2.0f, oxygenDecreaseRate);
    }

    void FixedUpdate()
    {
        if (health <= 0 || OxygenOut) return;

        KeyboardMovement();

        // Control for mobile devices
        if (Input.GetMouseButton(0))
        {
            LastClickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMoving = true;
            TouchOrClickMovement();
        }
    }

    void KeyboardMovement()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        float animationSpeed = Mathf.Abs(direction.x);
        animator.SetFloat("Speed", animationSpeed == 0 ? baseAnimationSpeed : moveAnimationSpeed);

        // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        if (direction.x * rigidBody.velocity.x < maxHorizontalSpeed)
        {
            rigidBody.AddForce(Vector2.right * direction.x * moveHorizontalForce);
        }

        if (direction.y * rigidBody.velocity.y < maxVerticalSpeed)
        {
            rigidBody.AddForce(Vector2.up * direction.y * moveVerticalForce);
        }

        // If the input is moving the player right and the player is facing left...
        if (direction.x > 0 && !facingRight || direction.x < 0 && facingRight)
            Flip();
    }

    void TouchOrClickMovement()
    {
        if (isMoving && (Vector2)transform.position != LastClickedPosition)
        {
            float step = maxHorizontalSpeed * Time.deltaTime;
            rigidBody.MovePosition(Vector2.MoveTowards(transform.position, LastClickedPosition, step));

        }
        else
        {
            isMoving = false;
        }

        animator.SetFloat("Speed", isMoving ? moveAnimationSpeed : baseAnimationSpeed);

        // If the input is moving the player right and the player is facing left...
        if (LastClickedPosition.x > transform.position.x && !facingRight || LastClickedPosition.x < transform.position.x && facingRight)
            Flip();
    }

    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void TakeHit()
    {
        if (hitTaken) return;

        hitTaken = true;

        health--;
        UIAnimator.SetInteger("HearthCount", health);

        if (health <= 0)
        {
            EventManager.TriggerEvent(EventConstants.OnPlayerDeath);
        }

        Invoke("TurnOffImunity", 2.5f);
    }

    void TurnOffImunity()
    {
        hitTaken = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Missiles"))
        {
            MissilesController missiles = collider.gameObject.GetComponent<MissilesController>();
            if (missiles != null && missiles.MissilesBlowed)
            {
                animator.SetTrigger("Hit");
                TakeHit();
            }
        }

        if (collider.CompareTag("WaterSurface"))
        {
            CancelInvoke("DecreaseOxygen");
            InvokeRepeating("IncreaseOxygen", 0.5f, 0.5f);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Missiles"))
        {
            MissilesController missiles = collider.gameObject.GetComponent<MissilesController>();
            if (missiles != null && missiles.MissilesBlowed)
            {
                animator.SetTrigger("Hit");
                TakeHit();
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("WaterSurface"))
        {
            CancelInvoke("IncreaseOxygen");
            InvokeRepeating("DecreaseOxygen", 1.0f, oxygenDecreaseRate);
        }
    }

    void IncreaseOxygen()
    {
        OxygenBar.IncrementProgress(increaseOxygenSpeed);
    }

    void DecreaseOxygen()
    {
        OxygenBar.DecrementProgress(decreaseOxygenSpeed);

        if (OxygenBar.GetSliderValue() <= 0)
        {
            EventManager.TriggerEvent(EventConstants.OnOxygenOut);
            OxygenOut = true;
        }
    }
}
