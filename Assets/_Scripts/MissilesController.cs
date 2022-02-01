using System.Collections;
using UnityEngine;

public class MissilesController : MonoBehaviour
{
    [SerializeField]
    float airMovementSpeed = 2.0f;

    [SerializeField]
    float waterMovementSpeed = 1.0f;

    [SerializeField]
    float readyModifierMinimum = 0.5f;

    [SerializeField]
    float readyModifierMaximum = 2.5f;

    Animator animator;

    bool readyToBlow = false;

    bool missilesBlowed = false;

    bool isInWater = false;

    bool stopMoving = false;

    public bool MissilesBlowed { get => missilesBlowed; }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        float readyModifier = Random.Range(readyModifierMinimum, readyModifierMaximum);
        animator.SetFloat("ReadyModifier", readyModifier);
    }

    void FixedUpdate()
    {
        if (stopMoving) return;

        if (!isInWater)
        {
            GetComponent<Rigidbody2D>().transform.position = new Vector2(transform.position.x, transform.position.y - airMovementSpeed * Time.deltaTime);
        }
        else
        {
            GetComponent<Rigidbody2D>().transform.position = new Vector2(transform.position.x, transform.position.y - waterMovementSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if ((collider.CompareTag("Player") || collider.CompareTag("OceanBottom")) && readyToBlow)
        {
            readyToBlow = false;
            StartCoroutine(DestroyObject());
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && readyToBlow)
        {
            readyToBlow = false;
            StartCoroutine(DestroyObject());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WaterSurface"))
        {
            isInWater = true;
        }
    }

    IEnumerator DestroyObject()
    {
        animator.SetBool("Blow", true);
        missilesBlowed = true;
        stopMoving = true;

        yield return new WaitForSeconds(0.8f);

        Destroy(gameObject);
    }

    void SetReadyToBlow()
    {
        readyToBlow = true;
    }
}
