using System;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    // Singleton instance
    public static PlayerBase Instance { get; private set; }

    public static event Action OnPlayerBaseDestroyed;

    private Health health;

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

        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        health.OnDeath += HandleBaseDestroyed;
    }

    private void OnDisable()
    {
        health.OnDeath -= HandleBaseDestroyed;
    }

    private void HandleBaseDestroyed()
    {
        print(gameObject.name + " : Destroyed");
        OnPlayerBaseDestroyed?.Invoke();
    }
}
