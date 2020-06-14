using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnPool : MonoBehaviour
{
    private GameObject[] columns;
    private GameObject[] coins;

    private Vector2 columnPoolPosition = new Vector2(-15f, -25f);
    private float timeSinceLastSpawned;
    private float spawnXPosition = 10f;
    private int currentColumn = 0;

    public int columnPoolSize = 16;

    public GameObject columnPrefab;
    public GameObject coinPrefab;
    public float deltaX = 0.2f;

    public float columnMin = -1f;
    public float columnMax = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        this.columns = new GameObject[this.columnPoolSize];
        this.coins = new GameObject[this.columnPoolSize];

        for (int i = 0; i < this.columnPoolSize; i++)
        {
            this.columns[i] = (GameObject)Instantiate(this.columnPrefab, this.columnPoolPosition, Quaternion.identity);
            this.coins[i] = (GameObject)Instantiate(this.coinPrefab, this.columnPoolPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.timeSinceLastSpawned += Time.deltaTime;
        if (GameController.instance.gameOver == false && this.timeSinceLastSpawned > GameController.instance.spawnRate)
        {
            this.timeSinceLastSpawned = 0;
           
            float spawnXPos = this.spawnXPosition + (this.deltaX * GameController.instance.level);
            float spawnYPosiotion = Random.Range(columnMin, columnMax);
            this.columns[this.currentColumn].transform.position = new Vector2(spawnXPos, spawnYPosiotion);


            float coinYPosiotion = Random.Range(columnMin, columnMax);
            this.coins[this.currentColumn].transform.position = new Vector2(spawnXPos + 5f, coinYPosiotion);

            this.updateCurrentColumn();
        }
    }

    private void updateCurrentColumn()
    {
        this.currentColumn = (this.currentColumn + 1) % this.columnPoolSize;
    }
}
