using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Singleton instance
    public static MenuManager Instance { get; private set; }

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

    private void DisplayEndGamePanel()
    {
        endGamePanel.SetActive(true);
    }
}
