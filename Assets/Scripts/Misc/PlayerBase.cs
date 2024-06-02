using System;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    // Singleton instance
    public static PlayerBase Instance { get; private set; }

    public static event Action OnPlayerBaseDestroyed;
    public static event Action<int> OnPlayerBaseHealthChanged;

    [SerializeField] private GameObject destroyedBasePrefab;

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
        health.OnHealthChanged += HandleBaseHealthChanged;
    }

    private void OnDisable()
    {
        health.OnDeath -= HandleBaseDestroyed;
        health.OnHealthChanged -= HandleBaseHealthChanged;
    }

    private void HandleBaseDestroyed()
    {
        print(gameObject.name + " : Destroyed");
        OnPlayerBaseDestroyed?.Invoke();

        Instantiate(destroyedBasePrefab, transform.position, transform.rotation);
    }

    private void HandleBaseHealthChanged(int newBaseHealth)
    {
        OnPlayerBaseHealthChanged?.Invoke(newBaseHealth);
    }
}
