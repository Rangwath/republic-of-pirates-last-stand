using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] private float timeToLive = 3f;

    private int damageAmount;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, timeToLive);
    }

    public void Fire(int cannonDamage, float cannonForce, Vector2 shipVelocity)
    {
        damageAmount = cannonDamage;

        Vector2 initialVelocity = (Vector2) transform.up * cannonForce + shipVelocity;

        rigidBody.AddForce(initialVelocity, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit : " + other.gameObject.name); 

        IHitable hitable = other.gameObject.GetComponent<IHitable>();
        hitable?.TakeHit();

        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
        damagable?.TakeDamage(damageAmount);

        if (hitable != null)
        {
            Destroy(gameObject);
        }
    }
}
