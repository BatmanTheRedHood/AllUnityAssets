using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.offset = transform.position - this.player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = this.player.transform.position + this.offset;
    }
}
