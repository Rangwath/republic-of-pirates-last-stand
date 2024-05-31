using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] private Sprite lowDamageSprite;
    [SerializeField] private Sprite highDamageSprite;

    private int startingHealth;

    private SpriteRenderer spriteRenderer;
    private Health health;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        health = GetComponent<Health>();
    }

    private void Start()
    {
        startingHealth = health.StartingHealth;
    }

    private void OnEnable()
    {
        health.OnHealthChanged += HandleHealthChanged;
    }

    private void OnDisable()
    {
        health.OnHealthChanged -= HandleHealthChanged;
    }

    private void HandleHealthChanged(int newCurrentHealth)
    {
        float healthPercentage = (float) newCurrentHealth / startingHealth * 100f;

        if (healthPercentage <= 60)
        {
            if (healthPercentage <= 30)
            {
                spriteRenderer.sprite = highDamageSprite;
                return;
            }
            spriteRenderer.sprite = lowDamageSprite;
        }
    }
}
