using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private int currentEnemyCount;
    public GameObject enemyPrefab;
    public float spawnInterval;
    public int maxEnemies;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (currentEnemyCount < maxEnemies)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, transform.rotation);
        currentEnemyCount++;
    }

    public void EnemyDied()
    {
        currentEnemyCount--;
    }
}
