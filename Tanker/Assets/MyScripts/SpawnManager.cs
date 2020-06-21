using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private int currentPosition = 0;
    private float timeLapsed = 10f;
    private AudioSource audioSource;

    public float spawnRate = 2f;
    public AudioClip spawnAudioClip;
    public ParticleSystem spawnEffectPrefab;

    public Transform[] spawnPositions;
    public GameObject[] enemyPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.timeLapsed > this.spawnRate)
        {
            // Instantiate enemy
            ParticleSystem spawnEffect = Instantiate(this.spawnEffectPrefab, this.spawnPositions[this.currentPosition].position, Quaternion.identity);
            spawnEffect.Play();
            // this.audioSource.PlayOneShot(this.spawnAudioClip);

            Invoke("SpawnEnemy", 1f);

            // Reset timeLapsed
            this.timeLapsed = 0f;
        }
        else
        {
            this.timeLapsed += Time.deltaTime;
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(this.enemyPrefabs[0], this.spawnPositions[this.currentPosition].position, Quaternion.identity);

        // Update Spawn position
        this.currentPosition = (this.currentPosition + 1) % this.spawnPositions.Length;
    }
}
