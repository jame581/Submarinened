using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject CreditsPanel;

    void Start()
    {
        if (CreditsPanel == null)
            Debug.LogError($"CreditsPanel reference missing on {gameObject.name}!", this);
        
        CreditsPanel.SetActive(false);
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
    }

    public void HideCredits()
    {
        CreditsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
