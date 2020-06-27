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

    public void Fire()
    {
        // Vector2 offset = this.playerMovement.lookDirection * 1.5f;
        GameObject bulletObject = Instantiate(this.bulletPrefab, this.firePosition.position /*+ new Vector3(offset.x, offset.y, 0)*/, Quaternion.identity);

        ParticleSystem muzzleBlazeEffect = Instantiate(this.muzzleBlaze, this.muzzleBlazePosition.position, Quaternion.identity);
        muzzleBlazeEffect.Play();
        this.audioSource.PlayOneShot(this.gunFireClip, 0.5f);

        // AutoDestroy https://gamedev.stackexchange.com/questions/151041/how-to-destroy-particles-system-after-its-work-is-over#:~:text=In%20Unity%20Version%202017.2%2C%20the,are%20no%20alive%20particles%20anymore.
        // Destroy(muzzleBlazeEffect, 2f);

        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.Launch(this.playerMovement.lookDirection, this.force);
    }
}
