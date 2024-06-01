using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    // Singleton instance
    public static GameMenuManager Instance { get; private set; }

    private const string MAIN_MENU_SCENE_NAME = "MainMenuScene";

    [SerializeField] private GameObject endGamePanel;

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

    private void OnEnable()
    {
        GameManager.OnEnemyWin += DisplayEndGamePanel;
    }

    private void OnDisable()
    {
        GameManager.OnEnemyWin -= DisplayEndGamePanel;
    }

    public void RestartCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE_NAME);
    }

    private void DisplayEndGamePanel()
    {
        endGamePanel.SetActive(true);
    }
}
