using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject player;
    private static float yLimit = -10.0f;

    public float speed = 3.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        this.player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (this.player.transform.position - transform.position).normalized;
        this.rb.AddForce(lookDirection * this.speed);

        if (transform.position.y < Enemy.yLimit)
        {
            Destroy(gameObject);
        }
    }
}
