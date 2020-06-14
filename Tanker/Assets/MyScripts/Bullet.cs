using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rgBody2d;

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
            // Debug.Log("Launch called!");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //we also add a debug log to know what the projectile touch
        Debug.Log("Projectile Collision with " + other.gameObject);

        // Check if enemy and damage enemy. Tag Damageble..Damager

        /*BackAndForth enemyController = other.gameObject.GetComponent<BackAndForth>();
        if (enemyController != null)
        {
            enemyController.Fix();
        }
        */

        Destroy(gameObject);
    }
}
