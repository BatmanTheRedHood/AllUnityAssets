using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject focalPoint;
    private bool hasPowerup;

    public float speed;
    public float powerUpDuration = 4;
    public float powerUpStrength = 10.0f;
    public GameObject powerupIndicator;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        this.focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        this.powerupIndicator.transform.position = transform.position + new Vector3(0, -0.3f, 0);

        this.rb.AddForce(this.focalPoint.transform.forward * this.speed * verticalInput);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp")) {
            this.hasPowerup = true;
            this.powerupIndicator.gameObject.SetActive(true);

            Destroy(other.gameObject);
            Invoke("DisablePower", this.powerUpDuration);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && this.hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRb.AddForce(awayFromPlayer * this.powerUpStrength, ForceMode.Impulse);
        }
    }

    private void DisablePower()
    {
        this.hasPowerup = false;
        this.powerupIndicator.gameObject.SetActive(false);
    }
}
