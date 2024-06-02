using UnityEngine;

public class WreckSpawner : MonoBehaviour
{
    [SerializeField] private GameObject wreckPrefab;

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
        Instantiate(wreckPrefab, transform.position, transform.rotation);
    }
}
