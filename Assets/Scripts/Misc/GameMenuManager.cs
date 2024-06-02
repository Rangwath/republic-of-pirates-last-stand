using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    // Singleton instance
    public static GameMenuManager Instance { get; private set; }

    private const string MAIN_MENU_SCENE_NAME = "MainMenuScene";

    [SerializeField] private float endGamePanelDisplayDelay = 4f;
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
        GameManager.OnGameOver += HandleGameOver;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= HandleGameOver;
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

    private void HandleGameOver()
    {
        StartCoroutine(DisplayEndGamePanelWithDelay());
    }

    private IEnumerator DisplayEndGamePanelWithDelay()
    {
        yield return new WaitForSeconds(endGamePanelDisplayDelay);
        endGamePanel.SetActive(true);
    }
}
