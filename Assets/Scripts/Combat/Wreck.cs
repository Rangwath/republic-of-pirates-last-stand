using System.Collections;
using UnityEngine;

public class Wreck : MonoBehaviour
{
    [SerializeField] private float timeToLive = 5f;
    [SerializeField] private float explosionsDelay = 0.25f;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Transform[] explosionPoints;

    private void Start()
    {
        Destroy(gameObject, timeToLive);
        StartCoroutine(ExplosionsRoutine());
    }

    private IEnumerator ExplosionsRoutine()
    {
        foreach (Transform explosionPoint in explosionPoints)
        {
            Instantiate(explosionPrefab, explosionPoint.position, Quaternion.identity);
            Hitable.OnWoodHit?.Invoke();
            yield return new WaitForSeconds(explosionsDelay);
        }
    }
}
