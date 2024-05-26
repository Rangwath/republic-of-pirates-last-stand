using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    public static event Action OnEnemyWin;

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
        PlayerController.OnPlayerDeath += HandleEnemyWin;
        PlayerBase.OnPlayerBaseDestroyed += HandleEnemyWin;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerDeath -= HandleEnemyWin;
        PlayerBase.OnPlayerBaseDestroyed -= HandleEnemyWin;
    }

    private void HandleEnemyWin()
    {
        Debug.Log("Enemies Won");
        OnEnemyWin?.Invoke();
    }
}
