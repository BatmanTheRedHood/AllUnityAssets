using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private int score = 0;
    private int coinCount = 0;
    private WaitForSeconds m_EndWait;           // Used to have a delay whilst the round or game ends.
    private bool isWaiting = false;
    private float originalScrollSpeed = 1.5f;
    private float originalSpawnRate = 5;

    [HideInInspector]
    public int level = 1;
    [HideInInspector]
    public float levelDelta = 0;

    [HideInInspector]
    public bool gameOver = false;

    [HideInInspector]
    public float scrollSpeed = 1.5f;
    
    public int levelUpCount = 5;
    [HideInInspector]
    public float spawnRate = 5;

    public float deltaSpeed = 0.3f;
    public float deltaSpawnRate = 0.1f;

    public float m_EndDelay = 3f;               // The delay between the end of RoundPlaying and RoundEnding phases.

    public GameObject gameOverText;
    public Text gameScore;
    public Text gameLevel;
    public Text coinCountText;

    // Start is called before the first frame update
    void Awake()
    {
        if (GameController.instance == null)
        {
            GameController.instance = this;
        } else if (GameController.instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        m_EndWait = new WaitForSeconds(m_EndDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isWaiting && this.gameOver && InputController.isScreenTap())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void BirdScored()
    {
        if (this.gameOver)
        {
            return;
        }

        this.score++;
        this.gameScore.text = "Score: " + this.score.ToString();

        this.updateLevel();
    }

    public void CoinCollected()
    {
        if (this.gameOver)
        {
            return;
        }

        this.coinCount++;
        this.coinCountText.text = ": " + this.coinCount.ToString();
    }

    public void BirdDied()
    {
        this.gameOverText.SetActive(true);
        this.gameOver = true;

        this.isWaiting = true;

        StartCoroutine(GameEnding());
    }   

    private void updateLevel()
    {
        if (this.score % this.levelUpCount == 0)
        {
            this.level++;

            float deltaPercentage = (100f - this.level) / 100f;
            this.levelDelta = this.level * deltaPercentage;

            this.scrollSpeed = this.originalScrollSpeed + (this.deltaSpeed * this.levelDelta);
            this.spawnRate = Mathf.Max(this.originalSpawnRate - (this.deltaSpawnRate * this.levelDelta), 1.5f);

            // Debug.Log("Delta and SpawnRate " + this.levelDelta + " " + this.spawnRate);
                
            this.gameLevel.text = "Level: " + this.level.ToString();
        }
    }


    private IEnumerator GameEnding()
    {
        // Wait for the specified length of time until yielding control back to the game loop.
        yield return m_EndWait;
        this.isWaiting = false;
    }
}
