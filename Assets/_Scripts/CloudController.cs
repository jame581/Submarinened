using UnityEngine;

public class CloudController : MonoBehaviour
{
    [Header("Sprites Settings")]
    [SerializeField]
    Sprite[] sprites;

    [Header("Movement Settings")]
    [SerializeField]
    float speedMin;

    [SerializeField]
    float speedMax;

    SpriteRenderer spriteRenderer;

    float movementSpeed;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        int spriteIndex = Random.Range(0, sprites.Length);
        spriteRenderer.sprite = sprites[spriteIndex];

        movementSpeed = Random.Range(speedMin, speedMax);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().transform.position = new Vector2(transform.position.x + movementSpeed * Time.deltaTime, transform.position.y);
    }
}
