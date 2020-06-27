using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private AudioSource audioSource;
    private Color objectColor = Color.green;
    private Color fadeColor = Color.gray;
    private float fadeStart = 0;

    public int life = 1;

    public AudioClip damageClip;
    public AudioClip destroyClip;

    // public ParticleSystem damageEffect;
    public ParticleSystem destoryEffect;

    public Renderer tankRenderer;

    // Start is called before the first frame update
    void Start()
    {
        this.fadeStart = this.life;
        this.audioSource = GetComponent<AudioSource>();

        if (this.life > 1)
        {
            tankRenderer.material.color = this.objectColor;
        }
    }

    public void TakeDamage(int amount)
    {
        this.life -= amount;

        if (this.life > 0)
        {
            if (this.damageClip != null)
                this.audioSource.PlayOneShot(this.damageClip);

            float fadeEffect = (this.fadeStart - this.life) / this.fadeStart;
            tankRenderer.material.color = Color.Lerp(objectColor, fadeColor, fadeEffect);
        } else
        {
            ParticleSystem particleS = Instantiate(this.destoryEffect, this.transform.position, Quaternion.identity);
            particleS.Play();

            ///Object gets destroyed before it plays.  So will set active to false immediately and destroy a little late.
            // Problem: https://answers.unity.com/questions/1213839/destroy-object-and-play-sound.html
            // this.audioSource.PlayOneShot(this.destroyClip);

            gameObject.SetActive(false);
            GameController.instance.DestroyGameObject(gameObject);
        }
    }
}
