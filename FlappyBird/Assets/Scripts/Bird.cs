using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private bool isDead;
    private Rigidbody2D rgBody;
    private Animator animator;
    private AudioSource audioSource;

    public float force = 200f;
    public AudioClip flyClip;
    public AudioClip hitClip;
    public AudioClip coinCollectClip;

    // Start is called before the first frame update
    void Start()
    {
        this.rgBody = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isDead || PauseMenu.isGamePaused)
        {
            return;
        }

        if (InputController.isScreenTap())
        {
            this.rgBody.velocity = Vector2.zero;
            this.rgBody.AddForce(Vector2.up * this.force);
            this.animator.SetTrigger("Flap");

            this.audioSource.PlayOneShot(this.flyClip);
        }
    }

    public void CollectCoin()
    {
        if (this.isDead)
            return;


        this.audioSource.PlayOneShot(this.coinCollectClip);
        GameController.instance.CoinCollected();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!this.isDead)
            this.audioSource.PlayOneShot(this.hitClip);

        this.rgBody.velocity = Vector2.zero;
        this.isDead = true;
        this.animator.SetTrigger("Die");


        GameController.instance.BirdDied();
    }
}
