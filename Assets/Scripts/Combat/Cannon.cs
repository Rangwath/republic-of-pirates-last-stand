using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private float cannonForce = 10f;
    [SerializeField] private CannonBall cannonBallPrefab;

    public void Fire(Vector2 shipVelocity, LayerMask shipLayerMask)
    {
        CannonBall cannonBall = Instantiate(cannonBallPrefab, transform.position, transform.rotation);
        cannonBall.gameObject.layer = shipLayerMask;
        cannonBall.Fire(shipVelocity, cannonForce);
    }
}
