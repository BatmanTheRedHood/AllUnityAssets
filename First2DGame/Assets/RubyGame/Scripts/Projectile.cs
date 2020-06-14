using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rigidBody2d;

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
        this.rigidBody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        if (this.rigidBody2d != null)
        this.rigidBody2d.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //we also add a debug log to know what the projectile touch
        Debug.Log("Projectile Collision with " + other.gameObject);

        BackAndForth enemyController = other.gameObject.GetComponent<BackAndForth>();
        if (enemyController != null)
        {
            enemyController.Fix();
        }

        Destroy(gameObject);
    }
}
