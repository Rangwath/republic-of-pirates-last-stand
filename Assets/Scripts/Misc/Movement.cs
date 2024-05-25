using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float accelerationForce = 3f;
    [SerializeField] private float steeringForce = 2f;
    [SerializeField] private float driftFactor = 0.5f;
    [SerializeField] private float maxSpeed = 5f;

    private Vector2 movement;
    private float rotationAngle;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplyForwardBackwardForce();
        DecreaseDrift();
        ApplySteering();
    }

    public void SetCurrentMovement(Vector2 currentMovement)
    {
        movement = currentMovement;
    }

    private void ApplyForwardBackwardForce()
    {
        rigidBody.AddForce(transform.up * movement.y * accelerationForce, ForceMode2D.Force);

        // Prevent going forward faster than max speed
        if (rigidBody.velocity.magnitude > maxSpeed)
        {
            rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;
        }

        // Prevent going backward
        if (Vector2.Dot(rigidBody.velocity, transform.up) < 0)
        {
            rigidBody.velocity = Vector2.zero;
        }
    }

    private void DecreaseDrift()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rigidBody.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rigidBody.velocity, transform.right);

        rigidBody.velocity = forwardVelocity + (rightVelocity * driftFactor);
    }

    private void ApplySteering()
    {
        // Only apply steering when moving forward
        if (Vector2.Dot(rigidBody.velocity, transform.up) > 0)
        {
            rotationAngle -= movement.x * steeringForce;
            rigidBody.MoveRotation(rotationAngle);
        }
    }
}
