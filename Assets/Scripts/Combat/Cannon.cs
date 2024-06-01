using System;
using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public static event Action OnCannonFire;

    [SerializeField] private int cannonDamage = 10;
    [SerializeField] private float cannonForce = 10f;
    [SerializeField] private float cannonFlashDuration = 0.1f;
    [SerializeField] private CannonBall cannonBallPrefab;
    [SerializeField] private GameObject cannonFlash;
    [SerializeField] private SpriteRenderer cannonSpriteRenderer;

    public void Fire(Vector2 shipVelocity, LayerMask shipLayerMask)
    {
        CannonBall cannonBall = Instantiate(cannonBallPrefab, transform.position, transform.rotation);
        cannonBall.gameObject.layer = shipLayerMask;

        cannonBall.Fire(cannonDamage, cannonForce, shipVelocity, cannonSpriteRenderer.sortingLayerID, cannonSpriteRenderer.sortingOrder);
        StartCoroutine(CannonFlashRoutine());

        OnCannonFire?.Invoke();
    }

    private IEnumerator CannonFlashRoutine()
    {
        cannonFlash.SetActive(true);
        yield return new WaitForSeconds(cannonFlashDuration);
        cannonFlash.SetActive(false);
    }
}
