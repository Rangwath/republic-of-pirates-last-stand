using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    public static event Action OnEnemyWin;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Slider playerHealthBarSlider;

    private int currentScore = 0;

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

    private void Start()
    {
        UpdateScore(0);
    }

    private void OnEnable()
    {
        PlayerController.OnPlayerDeath += HandleEnemyWin;
        PlayerController.OnPlayerHealthChanged += UpdatePlayerHealth;
        PlayerBase.OnPlayerBaseDestroyed += HandleEnemyWin;
        Health.OnScoreUpdated += UpdateScore;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerDeath -= HandleEnemyWin;
        PlayerController.OnPlayerHealthChanged -= UpdatePlayerHealth;
        PlayerBase.OnPlayerBaseDestroyed -= HandleEnemyWin;
        Health.OnScoreUpdated -= UpdateScore;
    }

    private void HandleEnemyWin()
    {
        Debug.Log("Enemies Won");
        OnEnemyWin?.Invoke();
    }

    private void UpdateScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        scoreText.text = currentScore.ToString();
        Debug.Log("Score Updated : " + currentScore);
    }

    private void UpdatePlayerHealth(int newPlayerHealth)
    {
        playerHealthBarSlider.value = newPlayerHealth;
    }
}
