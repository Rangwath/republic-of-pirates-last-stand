using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float initialSpawnRate = 5.0f;
    [SerializeField] private float spawnRateDecrease = 0.1f;
    [SerializeField] private float minimumSpawnRate = 0.5f;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemyTarget;
    [SerializeField] private Transform[] spawnPoints;
    
    private float currentSpawnRate;
    private float elapsedTime = 0f;

    private void Start()
    {
        currentSpawnRate = initialSpawnRate;
        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        // Gradually increase the spawn rate
        if (currentSpawnRate > minimumSpawnRate)
        {
            currentSpawnRate = initialSpawnRate - (elapsedTime * spawnRateDecrease);
            currentSpawnRate = Mathf.Max(currentSpawnRate, minimumSpawnRate);
        }

        // Debug.Log("Spawn Rate : " + currentSpawnRate);
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Wait for the current spawn rate duration
            yield return new WaitForSeconds(currentSpawnRate);

            // Select a random spawn point
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[spawnIndex];

            // Instantiate the enemy at the chosen spawn point
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            enemy.GetComponent<EnemyStateMachine>().SetTarget(enemyTarget);
        }
    }
}
