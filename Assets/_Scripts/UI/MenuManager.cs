using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject CreditsPanel;

    public GameObject ControlPanel;

    public TextMeshProUGUI HighScoreText;

    void Start()
    {
        if (CreditsPanel == null)
            Debug.LogError($"CreditsPanel reference missing on {gameObject.name}!", this);
        
        if (ControlPanel == null)
            Debug.LogError($"ControlPanel reference missing on {gameObject.name}!", this);

        if (HighScoreText == null)
            Debug.LogError($"HighScoreText reference missing on {gameObject.name}!", this);

        CreditsPanel.SetActive(false);
        ControlPanel.SetActive(true);

        float bestTime = PlayerPrefs.GetFloat("highscore_time", 0.0f);
        int highScore = PlayerPrefs.GetInt("highscore_score", 0);

        HighScoreText.text = $"Best time: {bestTime:0.0} s\nHigh score: {highScore}";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CreditsPanel.SetActive(false);
        }
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void ShowCredits()
    {
        CreditsPanel.SetActive(true);
        ControlPanel.SetActive(false);
    }

    public void HideCredits()
    {
        CreditsPanel.SetActive(false);
        ControlPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
