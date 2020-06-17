using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEmitter : MonoBehaviour
{
    public Rigidbody2D bullet;
    public Transform top;

    [Range(0, 1000)]
    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FireBullet", 1, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            FireBullet();
        }
    }

    void FireBullet()
    {
        Rigidbody2D rocketInstance;
        rocketInstance = Instantiate(bullet, top.position, top.rotation) as Rigidbody2D;
        rocketInstance.AddForce(transform.up * bulletSpeed);
    }
}
