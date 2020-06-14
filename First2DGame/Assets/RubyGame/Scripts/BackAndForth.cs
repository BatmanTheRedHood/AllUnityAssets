using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour
{
    private Rigidbody2D rigidBody2d;
    private AudioSource audioSource;

    private float timer;
    private int direction;
    private Animator animator;
    private bool broken;

    public float speed = 2f;
    public bool isVertical;
    public float changeTime = 2f;
    public ParticleSystem smokeEffect;

    public AudioClip[] enemyHitClips;
    public AudioClip fixedClip;

    // Start is called before the first frame update
    void Start()
    {
        this.broken = true;
        this.direction = 1;
        this.timer = changeTime;
        this.rigidBody2d = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.broken == false)
        {
            return;
        }

        if (this.timer < 0)
        {
            this.direction = -this.direction;
            this.timer = this.changeTime;
        }

        this.timer -= Time.deltaTime;

        if (isVertical)
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
        } else
        {
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
        }
    }

    private void FixedUpdate()
    {
        if (this.broken == false)
        {
            return;
        }

        Vector2 position = transform.position;
        if (isVertical)
        {
            position.y += this.speed * this.direction * Time.deltaTime;
        } else
        {
            position.x += this.speed * this.direction * Time.deltaTime;
        }

        this.rigidBody2d.MovePosition(position);
    }

    public void Fix()
    {
        this.broken = false;

        // Simulated property of the Rigidbody2d to false.
        // This removes the Rigidbody from the Physics System simulation, so it won’t be taken into account
        // by the system for collision, and the fixed robot won’t stop the Projectile anymore or be able to hurt the main character.
        this.rigidBody2d.simulated = false;

        this.animator.SetTrigger("Fixed");

        // Play the Scene. You can see that the smoke instantly disappears.
        // That’s because when a Particle System is destroyed, it destroys all the particles it was
        // currently handling, even the ones that were only just created. Stop, on the other hand, simply stops the Particle System
        // from creating particles, and the particles that already exist can finish their lifetime normally.
        // This looks a lot more natural than all of the particles disappearing at once.
        // Destroy(smokeEffect.gameObject)

        this.smokeEffect.Stop();
        this.audioSource.Stop();
        this.audioSource.PlayOneShot(fixedClip);
    }

    // Unlike your damage zone, you can’t use a Trigger because you want the enemy Collider to be “solid” and actually collide with things.
    // Thankfully, Unity offers a second set of functions!
    // Just like you used OnTriggerEnter2D, you can also use OnCollisionEnter2D, which is called when a Rigidbody collides with something.
    // In this case, OnCollisionEnter2D is called when your enemy collides with the world or your main character.
    // And just like you did for the damage zone, you can test to see if the enemy has collided with your main character. 
    private void OnCollisionEnter2D(Collision2D other)
    {
        RubyController rubyController = other.gameObject.GetComponent<RubyController>();

        if (rubyController != null)
        {
            rubyController.changeHealth(-1);
        }
    }
}
