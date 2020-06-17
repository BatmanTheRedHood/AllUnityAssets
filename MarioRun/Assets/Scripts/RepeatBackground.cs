using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    public float width;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        this.width = GetComponent<BoxCollider>().size.x;
        this.startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > this.width /2)
        {
            transform.position = this.startPos;
        }
    }
}
