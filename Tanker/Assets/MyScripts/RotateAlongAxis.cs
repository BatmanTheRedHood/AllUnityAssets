using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAlongAxis : MonoBehaviour
{
    public float rotationSpeed = 10;
    public Vector3 axis = new Vector3(0, 0, 1);

    // Update is called once per frame
    void Update()
    {
        float angle = this.rotationSpeed * Time.deltaTime;

        this.transform.Rotate(axis, angle);
    }
}
