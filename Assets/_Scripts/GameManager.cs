using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EventManager EventManager;

    void Awake()
    {
        if (EventManager == null)
        {
            EventManager = GetComponent<EventManager>();
            if (EventManager == null)
            {
                Debug.LogError("Event manage reference missing!", this);
            }
        }
    }

    void OnEnable()
    {
        EventManager.StartListening(EventConstants.OnPlayerDeath, GameOver);
        EventManager.StartListening(EventConstants.OnOxygenOut, GameOver);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventConstants.OnPlayerDeath, GameOver);
        EventManager.StopListening(EventConstants.OnOxygenOut, GameOver);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void GameOver()
    {
        Debug.Log("Game Over called!");

        EventManager.TriggerEvent(EventConstants.OnGameOver);
    }
}
