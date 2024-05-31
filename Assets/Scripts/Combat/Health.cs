using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable
{
    public static event Action<int> OnScoreUpdated;
    public static event Action OnWoodImpact;
    public static event Action OnStoneImpact;

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

    public void TakeHit()
    {
        if (IsGameObjectInLayerMask(gameObject, GameManager.Instance.WoodImpactLayer))
        {
            OnWoodImpact?.Invoke();
        }
        if (IsGameObjectInLayerMask(gameObject, GameManager.Instance.StoneImpactLayer))
        {
            OnStoneImpact?.Invoke();
        }
    }

    private void SetCurrentHealth(int newCurrentHealth)
    {
        currentHealth = newCurrentHealth;
        OnHealthChanged?.Invoke(currentHealth);
    }

    public bool IsGameObjectInLayerMask(GameObject gameObject, LayerMask layerMask)
    {
        // Convert the GameObject's layer to a bitmask and check if it matches the LayerMask
        return ((layerMask.value & (1 << gameObject.layer)) > 0);
    }
}
