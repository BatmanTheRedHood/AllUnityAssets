using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        this.startPosition = new Vector3(0, 0, 0);
        transform.position = this.startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
