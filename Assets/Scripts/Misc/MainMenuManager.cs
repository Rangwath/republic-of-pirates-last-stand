using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Singleton instance
    public static MainMenuManager Instance { get; private set; }

    private const string GAME_SCENE_NAME = "GameScene";

    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject introPanel;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null) 
        { 
            Instance = this; 
        }
        else if (Instance != this)
        {
            Debug.LogWarning("Another instance detected and destroyed");
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(GAME_SCENE_NAME);
    }

    public void DisplayIntroPanel()
    {
        introPanel.SetActive(true);
    }

    public void DisplayControlsPanel()
    {
        controlsPanel.SetActive(true);
    }

    public void HideControlsPanel()
    {
        controlsPanel.SetActive(false);
    }
}
