using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] private float timeToLive = 3f;

    private const int SORTING_ORDER_DECREASE = 10;

    private int damageAmount;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void Start()
    {
        Destroy(gameObject, timeToLive);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool hasHit = false;

        // Get the position of the collision
        Vector2 hitPosition = other.ClosestPoint(transform.position);

        foreach (IHitable hitable in other.gameObject.GetComponents<IHitable>())
        {
            hitable?.TakeHit(hitPosition);
            hasHit = true;
        }

        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
        damagable?.TakeDamage(damageAmount);

        if (hasHit)
        {
            Destroy(gameObject);
        }
    }

    public void Fire(int cannonDamage, float cannonForce, Vector2 shipVelocity, int sortingLayerID, int sortingOrder)
    {
        AssignSortingIDToAllRenderers(sortingLayerID, sortingOrder);

        damageAmount = cannonDamage;

        Vector2 initialVelocity = (Vector2) transform.up * cannonForce + shipVelocity;

        rigidBody.AddForce(initialVelocity, ForceMode2D.Impulse);
    }

    private void AssignSortingIDToAllRenderers(int sortingLayerID, int sortingOrder)
    {
        spriteRenderer.sortingLayerID = sortingLayerID;
        spriteRenderer.sortingOrder = sortingOrder - SORTING_ORDER_DECREASE;
        
        trailRenderer.sortingLayerID = sortingLayerID;
        trailRenderer.sortingOrder = sortingOrder - (SORTING_ORDER_DECREASE * 2);
    }
}
