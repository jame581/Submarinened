using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;

    [SerializeField]
    Transform spawnSpot;

    [SerializeField]
    float movementSpeed;

    [SerializeField]
    float movementSpeedModifier;

    [SerializeField]
    float fireRate;

    [SerializeField]
    bool flightFromLeftToRight = true;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnProjectile", Random.Range(0.5f, 2.0f), fireRate);

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer.flipX = !flightFromLeftToRight;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (flightFromLeftToRight)
        {
            transform.position = new Vector2(transform.position.x + (movementSpeed + GetModifier()) * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - (movementSpeed + GetModifier()) * Time.deltaTime, transform.position.y);
        }
    }

    void SpawnProjectile()
    {
        GameObject projectileObject = Instantiate(projectile, spawnSpot.position, Quaternion.identity);
    }

    float GetModifier()
    {
        return Random.Range(-movementSpeedModifier, movementSpeedModifier);
    }
}
