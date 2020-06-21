using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    private AudioSource audioSource;

    [HideInInspector]
    public Tilemap tilemap;

    public GameObject tilemapGameObject;
    public AudioClip tankExplosionClip;
    public AudioClip bulletStoneImpactClip;

    public ParticleSystem tankExplosionEffect;
    public GameObject gameOverPanelUI;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        this.tilemap = tilemapGameObject.GetComponent<Tilemap>();
        this.audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayBulletStoreImpact()
    {
        this.audioSource.PlayOneShot(this.bulletStoneImpactClip);
    }

    public void ExplodeTank(Vector3 position) 
    {
        ParticleSystem particleS = Instantiate(this.tankExplosionEffect, position, Quaternion.identity);
        particleS.Play();

        this.audioSource.PlayOneShot(this.tankExplosionClip);
    }

    public void BirdDied(GameObject bird)
    {
        this.ExplodeTank(bird.transform.position);
        Destroy(bird);

        Invoke("EndGame", 0.5f);
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        this.gameOverPanelUI.SetActive(true);
    }

    public void Restart()
    {
        // isGamePaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
