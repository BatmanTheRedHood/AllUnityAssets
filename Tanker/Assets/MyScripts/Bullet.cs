using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rgBody2d;

    public int bulletImpactMagnitude = 1;
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

                // https://answers.unity.com/questions/1572343/how-to-detect-on-which-exact-tile-a-collision-happ.html
                this.DestroySurroundingTiles(hitPosition);
                GameController.instance.tilemap.SetTile(GameController.instance.tilemap.layoutGrid.WorldToCell(hitPosition), null);

                Debug.Log("Projectile Collision with tile" + collision.gameObject);
            }
        } else if (!this.gameObject.tag.Contains(collision.gameObject.tag))
        {
            // Above to avoid Player to PlayerBullet collision and Enemy to EnemyBullet collision.
            // This should be controlled from 2D Physics Collision settings.

            // Game object collision
            if (collision.gameObject.GetComponent<Tilemap>() == null)
            {
                Damageable damageable = collision.gameObject.GetComponent<Damageable>();
                if (damageable != null)
                    damageable.TakeDamage(this.bulletImpactMagnitude);
            }
            else
            {
                // Bullet Tilemap base hit
                if (this.gameObject.tag.Contains("Player"))
                    GameController.instance.PlayBulletStoneImpact();
            }
        }

        Destroy(gameObject);
    }

    // Bug fix: 
    private void DestroySurroundingTiles(Vector3  hitPosition)
    {
        Vector3[] points = {
            new Vector3(hitPosition.x -.1f, hitPosition.y, hitPosition.z),
            new Vector3(hitPosition.x + .1f, hitPosition.y, hitPosition.z),
            new Vector3(hitPosition.x, hitPosition.y - .1f, hitPosition.z),
            new Vector3(hitPosition.x, hitPosition.y + .1f, hitPosition.z),
        };

        GameController.instance.tilemap.SetTile(GameController.instance.tilemap.layoutGrid.WorldToCell(points[0]), null);
        GameController.instance.tilemap.SetTile(GameController.instance.tilemap.layoutGrid.WorldToCell(points[1]), null);
        GameController.instance.tilemap.SetTile(GameController.instance.tilemap.layoutGrid.WorldToCell(points[2]), null);
        GameController.instance.tilemap.SetTile(GameController.instance.tilemap.layoutGrid.WorldToCell(points[3]), null);
    }
}
