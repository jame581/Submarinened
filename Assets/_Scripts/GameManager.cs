using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI TimeText;

    public EventManager EventManager;

    float timer = 0.0f;

    bool isGameOver = false;

    void Awake()
    {
        if (EventManager == null)
        {
            EventManager = GetComponent<EventManager>();

            if (EventManager == null)
                Debug.LogError($"Event manage reference missing on {gameObject.name}!", this);

            if (TimeText == null)
                Debug.LogError($"TimeText reference missing on {gameObject.name}!", this);
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
        if (!isGameOver)
            UpdateTimer();
    }

    void GameOver()
    {
        Debug.Log("Game Over called!");

        EventManager.TriggerEvent(EventConstants.OnGameOver);
    }

    void UpdateTimer()
    {
        timer += Time.deltaTime;
        TimeText.text = $"Time: {timer:0.0} s";
    }
}
