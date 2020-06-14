using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    private float horizontal;
    private float vertical;

    private Rigidbody2D rigidbody2d;
    private Animator animator;
    private AudioSource audioSource;

    private int health;

    private bool isInvincible;
    private float invincibleTimer;

    private Vector2 lookDirection = new Vector2(1, 0);

    public float timeInvincible = 2.0f;
    public int maxHealth = 5;
    public float speed = 3.0f;
    public GameObject bullet;

    public AudioClip fireClip;
    public AudioClip damageClip;

    // Start is called before the first frame update
    void Start()
    {
        // QualitySettings.vSyncCount = 0;
        // Application.targetFrameRate = 10;
        this.health = maxHealth;

        this.animator = GetComponent<Animator>();
        this.rigidbody2d = GetComponent<Rigidbody2D>();
        this.audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (this.isInvincible)
        {
            this.invincibleTimer += Time.deltaTime;
            if (this.invincibleTimer > this.timeInvincible)
            {
                this.isInvincible = false;
            }
        }

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(this.rigidbody2d.position + Vector2.up * 0.2f, this.lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider !=  null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;

        // Time.deltaTime. deltaTime, contained inside Time, is a variable that Unity fills with the time it takes for a frame to be rendered.
        // Your character now runs at the same speed, regardless of the number of frames rendered by our game. It’s now “frame independent”.
        position.x += this.speed * horizontal * Time.deltaTime;
        position.y += this.speed * vertical * Time.deltaTime;

        this.rigidbody2d.MovePosition(position);  
    }

    public void Launch()
    {
        GameObject projectileObject = Instantiate(bullet, this.rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(this.lookDirection, 300);

        this.PlaySound(this.fireClip);
        animator.SetTrigger("Launch");
    }

    public void changeHealth(int value)
    {
        if (value < 0)
        {
            if (this.isInvincible)
            {
                return;
            }

            animator.SetTrigger("Hit");
            this.isInvincible = true;
            this.invincibleTimer = 0;

            this.PlaySound(this.damageClip);
        }

        this.health = Mathf.Clamp(this.health + value, 0, this.maxHealth);

        UIHealthBar.instance.SetValue(this.health / (float)maxHealth);
    }

    public void PlaySound(AudioClip clip)
    {
        this.audioSource.PlayOneShot(clip);
    }
}
