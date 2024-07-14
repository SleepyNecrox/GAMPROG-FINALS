using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private int currentEnemyCount;
    public GameObject enemyPrefab;
    public float spawnInterval;
    public int maxEnemies;
    bool canSpawn = true;

    public Timer timer;

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    void Update()
    {
        if(timer.waveTime == 0)
        {
            canSpawn =  false;
        }
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

    }

    private void SpawnEnemy()
    {
        if(!canSpawn)
        return;

        Instantiate(enemyPrefab, new Vector3(Random.Range(-25f, 25f), Random.Range(0f, 0f), 25), Quaternion.identity);
        currentEnemyCount++;
    }

    public void EnemyDied()
    {
        currentEnemyCount--;
    }

}
