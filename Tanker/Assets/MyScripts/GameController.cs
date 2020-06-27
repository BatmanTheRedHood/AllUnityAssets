using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [HideInInspector]
    public float spawnRate = 2f;

    [HideInInspector]
    public int enemyCounter = 20;

    [HideInInspector]
    public int maxEnemyCount = 20;

    [HideInInspector]
    public Tilemap tilemap;

    public Text scoreText;
    public Text levelText;

    public Text levelChangeText;
    public GameObject LevelChangePanel;
    public GameObject[] playerLifesUI;

    public AudioSource audioSource;
    public GameObject tilemapGameObject;
    public AudioClip tankExplosionClip;
    public AudioClip bulletStoneImpactClip;
    public AudioClip levelChangeClip;
    public AudioClip gameOverClip;

    public AudioClip spawnAudioClip;
    public ParticleSystem spawnEffectPrefab;

    public ParticleSystem tankExplosionEffect;
    public GameObject gameOverPanelUI;
    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        this.tilemap = tilemapGameObject.GetComponent<Tilemap>();
        // this.audioSource = GetComponent<AudioSource>();

        this.enemyCounter = this.maxEnemyCount;
        StartCoroutine(StartLevel());
    }

    /*
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SceneManager.LoadScene(1);
        }
    }
    */

    public void PlayBulletStoneImpact()
    {
        this.audioSource.PlayOneShot(this.bulletStoneImpactClip);
    }

    public void DestroyGameObject(GameObject gameObject)
    {
        this.audioSource.PlayOneShot(this.tankExplosionClip);

        // Check if bird killed
        if (gameObject.CompareTag("Bird"))
        {
            this.BirdDied();
        }
        else if (gameObject.CompareTag("Player"))
        {
            this.PlayerDied();
            Destroy(gameObject);
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            // Bullet-Tank Collision
            this.ExplodeTank(gameObject.transform.position);
            Destroy(gameObject, 1f);
        }
        //} else
        //{
        //    // Bullet- Bullet Collision
        //    Debug.Log("This is not required as All bullet has destroy: " + collision.gameObject);
        //    Destroy(collision.gameObject);
        //}
    }

    public void ExplodeTank(Vector3 position) 
    {
        // ParticleSystem particleS = Instantiate(this.tankExplosionEffect, position, Quaternion.identity);
        // particleS.Play();

        // this.audioSource.PlayOneShot(this.tankExplosionClip);
        this.UpdateScore();
    }

    public void BirdDied()
    {
        // ParticleSystem particleS = Instantiate(this.tankExplosionEffect, bird.transform.position, Quaternion.identity);
        // particleS.Play();

        // this.audioSource.PlayOneShot(this.tankExplosionClip);

        // Destroy(bird);

        Invoke("EndGame", 0.5f);
    }

    public void PlayerDied()
    {
        DataCenter.playerLife--;

        if (DataCenter.playerLife > 0)
        {
            this.playerLifesUI[DataCenter.playerLife].SetActive(false);
            StartCoroutine(SpawnPlayer());
        } else
        {
            this.playerLifesUI[DataCenter.playerLife].SetActive(false);
            Invoke("EndGame", 0.5f);
        }
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        this.gameOverPanelUI.SetActive(true);
        this.audioSource.PlayOneShot(this.gameOverClip);
    }

    public void Restart()
    {
        // isGamePaused = false;
        Time.timeScale = 1f;
        DataCenter.ResetData();
        SceneManager.LoadScene(0);
    }

    public IEnumerator StartLevel()
    {
        // Show next level UI
        this.LevelChangePanel.SetActive(true);

        // Set Life UI
        for (int i=2; i >= 0; i--)
        {
            if (i >= DataCenter.playerLife)
            {
                this.playerLifesUI[i].SetActive(false);
            }
        }

        // Set Level and its Params;
        StartCoroutine(this.SpawnPlayer());

        this.scoreText.text = "Score: " + DataCenter.score.ToString();
        this.levelText.text = "Level: " + DataCenter.level.ToString();
        this.levelChangeText.text = "Level: " + DataCenter.level.ToString();
        yield return new WaitForSeconds(1.5f);

        this.spawnRate = Mathf.Max(0.5f, 2f - DataCenter.level * .1f);
        this.enemyCounter = 0;

        // Hide UI
        this.LevelChangePanel.SetActive(false);
    }

    public IEnumerator EndLevel()
    {
        // Show next level UI
        this.LevelChangePanel.SetActive(true);
        this.levelChangeText.text = "Level " + DataCenter.level.ToString() + " Complete!";

        this.audioSource.PlayOneShot(this.levelChangeClip);
        // Wait for the specified length of time until yielding control back to the game loop.
        yield return new WaitForSeconds(3f);
        
        // Goto next level
        DataCenter.level++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator SpawnPlayer()
    {
        // yield return new WaitForSeconds(.3f);

        this.audioSource.PlayOneShot(this.spawnAudioClip, 0.4f);
        ParticleSystem particleS = Instantiate(this.spawnEffectPrefab, this.playerPrefab.transform.position, Quaternion.identity);
        particleS.Play();

        yield return new WaitForSeconds(.8f);

        Instantiate(this.playerPrefab, this.playerPrefab.transform.position, this.playerPrefab.transform.rotation);
    }

    private void UpdateScore()
    {
        DataCenter.score++;
        this.scoreText.text = "Score: " + DataCenter.score.ToString();

        // Game win condition.
        if (DataCenter.score % this.maxEnemyCount == 0)
        {
            StartCoroutine(EndLevel());
        }
    }
}
