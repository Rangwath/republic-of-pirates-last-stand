using System;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public static event Action OnCannonFire;

    [SerializeField] private int cannonDamage = 10;
    [SerializeField] private float cannonForce = 10f;
    [SerializeField] private CannonBall cannonBallPrefab;

    public void Fire(Vector2 shipVelocity, LayerMask shipLayerMask)
    {
        CannonBall cannonBall = Instantiate(cannonBallPrefab, transform.position, transform.rotation);
        cannonBall.gameObject.layer = shipLayerMask;
        cannonBall.Fire(cannonDamage, cannonForce, shipVelocity);
        OnCannonFire?.Invoke();
    }
}
