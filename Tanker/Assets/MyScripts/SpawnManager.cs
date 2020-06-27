using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private int currentPosition = 0;
    private float timeLapsed = 10f;
    private AudioSource audioSource;

    public AudioClip spawnAudioClip;
    public ParticleSystem spawnEffectPrefab;

    public Transform[] spawnPositions;
    public EnemyDetail[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.timeLapsed > GameController.instance.spawnRate)
        {
            if (GameController.instance.enemyCounter < GameController.instance.maxEnemyCount)
            {
                // Instantiate enemy
                ParticleSystem spawnEffect = Instantiate(this.spawnEffectPrefab, this.spawnPositions[this.currentPosition].position, Quaternion.identity);
                spawnEffect.Play();
                // this.audioSource.PlayOneShot(this.spawnAudioClip);

                Debug.Log("Enemy count: " + GameController.instance.enemyCounter);

                GameController.instance.enemyCounter++;
                Invoke("SpawnEnemy", 1f);

                // Reset timeLapsed
                this.timeLapsed = 0f;
            }
        }
        else
        {
            this.timeLapsed += Time.deltaTime;
        }
    }

    private void SpawnEnemy()
    {
        int randomInt = Random.Range(0, this.enemies.Length);
        Instantiate(this.GetNextEnemy(), this.spawnPositions[this.currentPosition].position, Quaternion.identity);

        // Update Spawn position
        this.currentPosition = (this.currentPosition + 1) % this.spawnPositions.Length;
    }

    private GameObject GetNextEnemy()
    {
        foreach(var enemyDetail in this.enemies)
        {
            if (enemyDetail.count > 0)
            {
                enemyDetail.count--;
                return enemyDetail.prefab;
            }
        }

        // this.ResetEnemy();
        // return GetNextEnemy();
        return null;
    }

    private void ResetEnemy()
    {
        this.enemies[2].count = DataCenter.level;
        this.enemies[1].count = Mathf.Min(20 - this.enemies[2].count,  2 * DataCenter.level);
        this.enemies[0].count = Mathf.Max(0, 20 - this.enemies[1].count - this.enemies[2].count);
    }
}

[System.Serializable]
public class EnemyDetail
{
    public int count;
    public GameObject prefab;
}
