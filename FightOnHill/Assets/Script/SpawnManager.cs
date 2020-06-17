using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnRange = 9.0f;
    private int waveNumber = 1;

    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    // Start is called before the first frame update
    void Start()
    {
        this.SpawnEnemyWave(this.waveNumber);
        Instantiate(this.powerupPrefab, generateRandomPosition(), this.powerupPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        int enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            this.waveNumber++;
            this.SpawnEnemyWave(this.waveNumber);
            Instantiate(this.powerupPrefab, generateRandomPosition(), this.powerupPrefab.transform.rotation);
        }
    }

    private void SpawnEnemyWave(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(this.enemyPrefab, generateRandomPosition(), this.enemyPrefab.transform.rotation);
        }
    }

    private Vector3 generateRandomPosition()
    {
        float randomX = Random.Range(-this.spawnRange, this.spawnRange);
        float randomZ = Random.Range(-this.spawnRange, this.spawnRange);

        return new Vector3(randomX, 0, randomZ);
    }

}
