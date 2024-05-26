using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable
{
    public event Action OnDeath;

    [SerializeField] private int startingHealth = 5;

    private int currentHealth;

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        Debug.Log(gameObject.name + " : Remaining Health : " + currentHealth);

        if (currentHealth <= 0) 
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }

    public void TakeHit()
    {
        Debug.Log(gameObject.name + " : Hit Taken -> Taking Damage");
    }
}
