using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public float startDelay;
    public float repeatInterval;
    public float obstacleWidth;
    public GameObject[] gameObjects;

    private Vector3 startPosition;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        this.startPosition = new Vector3(10 , 0, 6);
        this.playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        InvokeRepeating("InitializeObstacle", this.startDelay, this.repeatInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeObstacle()
    {
        if (!this.playerController.isGameOver)
        {
            int obstacleIndex = Random.Range(0, this.gameObjects.Length);
            GameObject obstacle = Instantiate(this.gameObjects[obstacleIndex], this.startPosition, Quaternion.identity); //this.gameObjects[obstacleIndex].transform.rotation);
            // Destroy(obstacle, 4);
        }
    }
}
