using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    private Vector2 columnPoolPosition = new Vector2(-15f, -25f);

    public ParticleSystem sparkle;
    public AudioClip audioClip;

    // Enable the Is Trigger property checkbox. Now when you test your game, the character will go through the health collectible.
    // The Physics System registers the collision, but because there is no code to handle it yet, our game doesn’t react to the collision.
    void OnTriggerEnter2D(Collider2D other)
    {
        Bird bird = other.GetComponent<Bird>();

        if (bird != null)
        {
            ParticleSystem particleS = Instantiate(this.sparkle, transform.position, Quaternion.identity);
            particleS.Play();

            bird.CollectCoin();

            transform.position = this.columnPoolPosition;
            //Destroy(gameObject); //Destroy is a built-in Unity function that destroys whatever you pass to it as a parameter —  in this case, gameObject. This is the GameObject that the script is attached to (the collectible health pack).
        }
    }
}
