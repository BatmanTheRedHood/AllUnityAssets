using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MovementController : NetworkBehaviour
{

    public float speed = 5f;
    public float turnSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        float horizontalMovement = Input.GetAxis("Horizontal");
        float forwardInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * forwardInput * this.speed * Time.deltaTime);
        transform.Rotate(Vector3.up, this.turnSpeed * horizontalMovement * Time.deltaTime);
    }

}
