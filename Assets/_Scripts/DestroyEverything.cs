using UnityEngine;

public class DestroyEverything : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collider)
    {
        Destroy(collider.gameObject);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }
}
