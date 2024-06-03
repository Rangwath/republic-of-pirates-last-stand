using UnityEngine;

public class WreckSpawner : MonoBehaviour
{
    [SerializeField] private GameObject wreckPrefab;
    [SerializeField] private TrailRenderer trailRenderer;

    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        health.OnDeath += SpawnWreck;
    }

    private void OnDisable()
    {
        health.OnDeath -= SpawnWreck;
    }

    private void SpawnWreck()
    {
        GameObject wreck = Instantiate(wreckPrefab, transform.position, transform.rotation);
        trailRenderer.transform.SetParent(wreck.transform);
    }
}
