using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private bool isOnGround;
    private Animator playerAnimator;
    private AudioSource audioSource;

    public float jumpForce;
    public float gravityModifier;
    public bool isGameOver;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= this.gravityModifier;
        this.rb = GetComponent<Rigidbody>();
        this.isOnGround = true;
        this.playerAnimator = GetComponent<Animator>();
        this.audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && this.isOnGround && !this.isGameOver)
        {
            this.rb.AddForce(Vector3.up * this.jumpForce, ForceMode.Impulse);
            this.isOnGround = false;
            this.playerAnimator.SetTrigger("Jump_trig");
            this.dirtParticle.Stop();

            this.audioSource.PlayOneShot(this.jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            this.isOnGround = true;
            this.dirtParticle.Play();
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            this.isGameOver = true;

            this.playerAnimator.SetBool("Death_b", true);
            this.playerAnimator.SetInteger("DeathType_int", 1);

            this.explosionParticle.Play();
            this.dirtParticle.Stop();

            this.audioSource.PlayOneShot(this.crashSound, 1.0f);
        }
    }
}
