using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private AudioSource audioSource;

    public float force = 500f;
    public GameObject bulletPrefab;
    public Transform firePosition;

    public ParticleSystem muzzleBlaze;
    public Transform muzzleBlazePosition;
    public AudioClip gunFireClip;

    // Start is called before the first frame update
    void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
        this.playerMovement = this.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        // Vector2 offset = this.playerMovement.lookDirection * 1.5f;
        GameObject bulletObject = Instantiate(this.bulletPrefab, this.firePosition.position /*+ new Vector3(offset.x, offset.y, 0)*/, Quaternion.identity);

        ParticleSystem muzzleBlazeEffect = Instantiate(this.muzzleBlaze, this.muzzleBlazePosition.position, Quaternion.identity);
        muzzleBlazeEffect.Play();
        this.audioSource.PlayOneShot(this.gunFireClip);

        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.Launch(this.playerMovement.lookDirection, this.force);
    }
}
