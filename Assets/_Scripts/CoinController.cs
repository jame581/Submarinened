using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField]
    int CoinValue = 1;

    [SerializeField]
    EventManager eventManager;

    AudioSource audioSource;

    Animator animator;

    void Awake()
    {
        if (eventManager == null)
        {
            GameObject gameManagerObject = GameObject.FindGameObjectWithTag("GameController");

            if (gameManagerObject == null)
                Debug.LogError($"EventManager reference missing on {gameObject.name}!", this);

            eventManager = gameManagerObject.GetComponent<EventManager>();

            if (eventManager == null)
                Debug.LogError($"EventManager reference missing on {gameObject.name}!", this);
        }

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            Debug.LogError($"AudioSource reference missing on {gameObject.name}!", this);

        animator = GetComponent<Animator>();

        if (animator == null)
            Debug.LogError($"Animator reference missing on {gameObject.name}!", this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            eventManager.TriggerEvent(EventConstants.OnCoinPickUp);
            audioSource.Play();
            animator.SetTrigger("CoinPickedUp");
            Destroy(gameObject, 0.8f);
        }
    }

}
