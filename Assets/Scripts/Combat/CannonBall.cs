using UnityEngine;

public class CannonBall : MonoBehaviour
{
    // [SerializeField] private int damageAmount = 1;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Fire(Vector2 shipVelocity, float cannonForce)
    {
        Vector2 initialVelocity = (Vector2) transform.up * cannonForce + shipVelocity;

        rigidBody.AddForce(initialVelocity, ForceMode2D.Impulse);
    }
}
