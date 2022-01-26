using UnityEngine;

public class SubmarineController : MonoBehaviour
{
    [SerializeField]
    float moveHorizontalForce = 100f; // Amount of force added to move the player
    
    [SerializeField]
    float moveVerticalForce = 100f; // Amount of force added to move the player

    [SerializeField]
    float maxHorizontalSpeed = 2f;
    
    [SerializeField]
    float maxVerticalSpeed = 2f;

    [SerializeField]
    bool facingRight;   // For determining which way the player is currently facing.

    Rigidbody2D rigidBody;

    Vector2 direction;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (rigidBody == null) return;

        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

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
    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
