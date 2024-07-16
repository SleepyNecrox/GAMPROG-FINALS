using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval;
    [SerializeField] private int maxEnemies;
    [SerializeField] private bool canSpawn = false;

    [SerializeField] private Timer timer;
    private int currentEnemyCount;

    private bool EnemiesIncreased = false;

    private void Update()
    {
        if(timer.currentTime == 0)
        {
            canSpawn =  false;
        }

        if (timer.currentTime == 60 && !EnemiesIncreased)
        {
            EnemiesIncreased = true;
            IncreaseEnemies();
        }
    }

    internal void StartWave()
    {
        //Debug.Log("Started Wave");
        canSpawn = true;
        StartCoroutine(SpawnWave());
    }


    private IEnumerator SpawnWave()
    {
        while (canSpawn)
        {
            if (currentEnemyCount < maxEnemies)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
            canSpawn = false;
            EnemiesIncreased = false;

    }

    private void SpawnEnemy()
    {
        if(!canSpawn)
        return;

        EnemyAI enemyAI = enemyPrefab.GetComponent<EnemyAI>();
        enemyAI.SetStats(timer.waveNumber);

        Instantiate(enemyPrefab, new Vector3(Random.Range(-25f, 25f), Random.Range(0f, 0f), 25), Quaternion.identity);
        currentEnemyCount++;
    }

    internal void EnemyDied()
    {
        currentEnemyCount--;
    }

    private void IncreaseEnemies()
    {
        maxEnemies += 2 * timer.waveNumber;
    }

}
