using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private PlayerController playerController;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        this.playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        this.speed = 15;
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.playerController.isGameOver)
        {
            transform.Translate(Vector3.right * this.speed * Time.deltaTime);

            if (gameObject.transform.position.x > 60 && gameObject.CompareTag("Obstacle"))
            {
                Destroy(gameObject);
            }
        }
    }
}
