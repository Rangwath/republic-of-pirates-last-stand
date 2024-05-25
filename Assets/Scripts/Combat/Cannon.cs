using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private float cannonForce = 10f;
    [SerializeField] private CannonBall cannonBallPrefab;

    private void OnEnable()
    {
        PlayerController.OnFireCannons += Fire;
    }

    private void OnDisable()
    {
        PlayerController.OnFireCannons -= Fire;
    }

    private void Fire(Vector2 shipVelocity)
    {
        Debug.Log("FIRE! " + this.gameObject.name);

        CannonBall cannonBall = Instantiate(cannonBallPrefab, transform.position, transform.rotation);

        cannonBall.Fire(shipVelocity, cannonForce);
    }
}
