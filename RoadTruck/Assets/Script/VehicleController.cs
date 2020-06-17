using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public float speed;
    public float turnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float forwardInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * forwardInput * this.speed * Time.deltaTime);
        transform.Rotate(Vector3.up, this.turnSpeed * horizontalMovement * Time.deltaTime);
    }
}
