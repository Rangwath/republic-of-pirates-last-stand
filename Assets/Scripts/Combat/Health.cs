using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable
{
    public static event Action<int> OnScoreUpdated;
    public event Action OnDeath;
    public event Action<int> OnHealthChanged;

    [SerializeField] private int startingHealth = 100;
    [SerializeField] private int scoreOnDeath = 10;

    private int currentHealth;

    private void Start()
    {
        SetCurrentHealth(startingHealth);
    }

    public void TakeDamage(int damageAmount)
    {
        SetCurrentHealth(currentHealth - damageAmount);

        Debug.Log(gameObject.name + " : Remaining Health : " + currentHealth);

        if (currentHealth <= 0) 
        {
            OnDeath?.Invoke();
            OnScoreUpdated?.Invoke(scoreOnDeath);
            Destroy(gameObject);
        }
    }

    public void TakeHit(Vector2 hitPosition)
    {
        // Health only takes damage, other Hit related processes should be in Hitable
    }

    private void SetCurrentHealth(int newCurrentHealth)
    {
        currentHealth = newCurrentHealth;
        OnHealthChanged?.Invoke(currentHealth);
    }
}
