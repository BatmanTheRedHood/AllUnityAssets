using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    private Rigidbody2D rgBody;

    // Start is called before the first frame update
    void Start()
    {
        this.rgBody = GetComponent<Rigidbody2D>();
        this.rgBody.velocity = Vector2.left * GameController.instance.scrollSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.rgBody.velocity != Vector2.left * GameController.instance.scrollSpeed)
        {
            this.rgBody.velocity = Vector2.left * GameController.instance.scrollSpeed;
        }

        if (GameController.instance.gameOver)
        {
            this.rgBody.velocity = Vector2.zero;
        }
    }
}
