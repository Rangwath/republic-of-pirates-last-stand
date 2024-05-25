using UnityEngine;

public interface IDamagable : IHitable
{
    void TakeDamage(int damageAmount);
}