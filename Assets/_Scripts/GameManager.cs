using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI TimeText; 
    
    [SerializeField]
    TextMeshProUGUI GameOverText;

    [SerializeField]
    GameObject InGameMenuPanel;
    
    [SerializeField]
    GameObject GameOverPanel;

    public EventManager EventManager;

    float timer = 0.0f;

    int score = 0;

    bool isGameOver = false;

    void Awake()
    {
        if (EventManager == null)
        {
            EventManager = GetComponent<EventManager>();

            if (EventManager == null)
                Debug.LogError($"Event manage reference missing on {gameObject.name}!", this);

        }

        if (TimeText == null)
            Debug.LogError($"TimeText reference missing on {gameObject.name}!", this);

        if (TimeText == null)
            Debug.LogError($"TimeText reference missing on {gameObject.name}!", this);
        
        if (GameOverText == null)
            Debug.LogError($"GameOverText reference missing on {gameObject.name}!", this);

        if (InGameMenuPanel == null)
            Debug.LogError($"InGameMenuPanel reference missing on {gameObject.name}!", this);
        
        if (GameOverPanel == null)
            Debug.LogError($"GameOverPanel reference missing on {gameObject.name}!", this);

        InGameMenuPanel.SetActive(false);
        GameOverPanel.SetActive(false);
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
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (InGameMenuPanel.activeInHierarchy)
            {
                HideInGameMenu();
            }
            else
            {
                ShowInGameMenu();
            }
        }

        if (!isGameOver)
            UpdateTimer();
    }

    void GameOver()
    {
        Debug.Log("Game Over called!");

        EventManager.TriggerEvent(EventConstants.OnGameOver);

        GameOverPanel.SetActive(true);
        Time.timeScale = 0;

        GameOverText.text = $"Time: {timer:0.0} s"; /*\nYour score: {score}*/
    }

    void UpdateTimer()
    {
        timer += Time.deltaTime;
        TimeText.text = $"Time: {timer:0.0} s";
    }

    public void ShowInGameMenu()
    {
        Time.timeScale = 0;
        InGameMenuPanel.SetActive(true);
    }

    public void HideInGameMenu()
    {
        InGameMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
