using System.Collections;
using UnityEngine;

public class MissilesController : MonoBehaviour
{
    Animator animator;

    bool readyToBlow = false;

    bool missilesBlowed = false;

    public bool MissilesBlowed { get => missilesBlowed; }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && readyToBlow)
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

    IEnumerator DestroyObject()
    {
        animator.SetBool("Blow", true);
        missilesBlowed = true;

        yield return new WaitForSeconds(0.8f);

        Destroy(gameObject);
    }

    void SetReadyToBlow()
    {
        readyToBlow = true;
    }
}
