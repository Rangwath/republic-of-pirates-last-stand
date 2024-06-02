using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    public static event Action OnGameStart;
    public static event Action OnGameOver;

    [field: SerializeField] public LayerMask WoodImpactLayer { get; private set; }
    [field: SerializeField] public LayerMask StoneImpactLayer { get; private set; }

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text finalTimerText;
    [SerializeField] private Slider playerHealthBarSlider;
    [SerializeField] private Slider playerBaseHealthBarSlider;

    private int currentScore = 0;
    private float startTime = 0;
    private bool hasGameEnded = false;

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
        OnGameStart?.Invoke();
        UpdateScore(0);
        startTime = Time.time;
    }

    private void Update()
    {
        if (!hasGameEnded)
        {
            UpdateTimer();
        }
    }

    private void OnEnable()
    {
        PlayerController.OnPlayerDeath += HandleGameOver;
        PlayerController.OnPlayerHealthChanged += UpdatePlayerHealth;
        
        PlayerBase.OnPlayerBaseDestroyed += HandleGameOver;
        PlayerBase.OnPlayerBaseHealthChanged += UpdatePlayerBaseHealth;
        
        Health.OnScoreUpdated += UpdateScore;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerDeath -= HandleGameOver;
        PlayerController.OnPlayerHealthChanged -= UpdatePlayerHealth;
        
        PlayerBase.OnPlayerBaseDestroyed -= HandleGameOver;
        PlayerBase.OnPlayerBaseHealthChanged -= UpdatePlayerBaseHealth;
        
        Health.OnScoreUpdated -= UpdateScore;
    }

    private void HandleGameOver()
    {
        Debug.Log("Game Over");
        hasGameEnded = true;
        finalScoreText.text = scoreText.text;
        finalTimerText.text = timerText.text;

        OnGameOver?.Invoke();
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

    private void UpdatePlayerBaseHealth(int newBaseHealth)
    {
        playerBaseHealthBarSlider.value = newBaseHealth;
    }

    private void UpdateTimer()
    {
        float time = Time.time - startTime;
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);

        if (seconds < 10)
        {
            timerText.text = minutes.ToString() + ":0" + seconds.ToString();
        }
        else
        {
            timerText.text = minutes.ToString() + ":" + seconds.ToString();
        }
    }
}
