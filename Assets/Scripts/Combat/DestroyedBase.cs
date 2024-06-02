using System.Collections;
using UnityEngine;

public class DestroyedBase : MonoBehaviour
{
    [SerializeField] private float timeBetweenExplosions = 0.2f;
    [SerializeField] private float woodExplosionsDelay = 0.1f;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Transform[] stoneExplosionPoints;
    [SerializeField] private Transform[] woodExplosionPoints;
    
    private void Start()
    {
        StartCoroutine(StoneExplosionsRoutine());
        StartCoroutine(WoodExplosionsRoutine());
    }

    private IEnumerator StoneExplosionsRoutine()
    {
        foreach (Transform explosionPoint in stoneExplosionPoints)
        {
            Instantiate(explosionPrefab, explosionPoint.position, Quaternion.identity);
            Hitable.OnStoneHit?.Invoke();
            yield return new WaitForSeconds(timeBetweenExplosions);
        }
    }

    private IEnumerator WoodExplosionsRoutine()
    {
        yield return new WaitForSeconds(woodExplosionsDelay);

        foreach (Transform explosionPoint in woodExplosionPoints)
        {
            Instantiate(explosionPrefab, explosionPoint.position, Quaternion.identity);
            Hitable.OnWoodHit?.Invoke();
            yield return new WaitForSeconds(timeBetweenExplosions);
        }
    }
}
