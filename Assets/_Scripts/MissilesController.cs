using System.Collections;
using UnityEngine;

public class MissilesController : MonoBehaviour
{
    GameObject smokeEffect;

    void Awake()
    {
        smokeEffect = transform.GetChild(0).gameObject;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            StartCoroutine(DestroyObject());
        }
    }

    IEnumerator DestroyObject()
    {
        if (smokeEffect)
        {
            smokeEffect.SetActive(true);
        }

        yield return new WaitForSeconds(0.8f);

        Destroy(gameObject);
    }
}
