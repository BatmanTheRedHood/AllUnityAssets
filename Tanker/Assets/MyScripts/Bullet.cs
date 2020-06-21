using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rgBody2d;

    public ParticleSystem bulletImpact;

    // Start is called before the first frame update

    /*void Start()
    {
        this.rigidBody2d = GetComponent<Rigidbody2D>();
    }
    */

    // If you double-click on the null reference exception error, it will open your Projectile script at the line
    // Rigidbody2d.AddForce (direction * force), which means your Rigidbody2d variable is empty (contains null), despite us
    // getting the Rigidbody in Start. 
    // That’s because Unity doesn’t run Start when you create the object, but on the next frame.
    // So when you call Launch on your projectile, just Instantiate and don’t call Start, so your Rigidbody2d is still empty.
    // To fix that, rename the void Start() function in the Projectile script to void Awake(). 
    // Contrary to Start, Awake is called immediately when the object is created (when Instantiate is called), so Rigidbody2d is
    // properly initialized before calling Launch.
    void Awake()
    {
        this.rgBody2d = GetComponent<Rigidbody2D>();

        // Debug.Log("Tilemap: " + tilemap);
    }

    private void Update()
    {
        if (transform.position.magnitude > 100.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        if (this.rgBody2d != null)
        {
            //Debug.Log("Direction: " + direction);
            this.rgBody2d.AddForce(direction.normalized * force);
            this.rgBody2d.rotation = InputController.GetAngle(direction);
            // Debug.Log("Launch called!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //we also add a debug log to know what the projectile touch
        Debug.Log("Projectile Collision with " + collision.gameObject);

        // Check if enemy and damage enemy. Tag Damageble..Damager

        /*BackAndForth enemyController = other.gameObject.GetComponent<BackAndForth>();
        if (enemyController != null)
        {
            enemyController.Fix();
        }
        */

        ParticleSystem bulletImpactEffect = Instantiate(this.bulletImpact, collision.contacts[0].point, Quaternion.identity);
        bulletImpactEffect.Play();

        Vector3 hitPosition = Vector3.zero;
        if (GameController.instance.tilemap != null && GameController.instance.tilemapGameObject == collision.gameObject)
        {
            Debug.Log("Projectile Collision with tile map contacts: " + collision.contacts);

            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                GameController.instance.tilemap.SetTile(GameController.instance.tilemap.WorldToCell(hitPosition), null);

                Debug.Log("Projectile Collision with tile" + collision.gameObject);
            }
        } else if (!this.gameObject.tag.Contains(collision.gameObject.tag))
        {
            // Game object collision
            if (collision.gameObject.GetComponent<Tilemap>() == null)
            {
                // Check if bird killed
                if (collision.gameObject.CompareTag("Bird"))
                {
                    GameController.instance.BirdDied(collision.gameObject);
                }
                else if (collision.gameObject.CompareTag("Player"))
                {
                    GameController.instance.BirdDied(collision.gameObject);
                }
                else
                {
                    // Bullet-Bullet Bullet-Tank Collision
                    GameController.instance.ExplodeTank(collision.gameObject.transform.position);
                    Destroy(collision.gameObject);
                }
            }
            else
            {
                // Tilemap base hit
                GameController.instance.PlayBulletStoreImpact();
            }
        }

        Destroy(gameObject);
    }
}
