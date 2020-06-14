﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public float force = 500f;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        this.playerMovement = this.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        Vector2 offset = this.playerMovement.lookDirection * 1.5f;
        GameObject bulletObject = Instantiate(this.bulletPrefab, this.transform.position + new Vector3(offset.x, offset.y, 0), Quaternion.identity);

        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.Launch(this.playerMovement.lookDirection, this.force);
    }
}
