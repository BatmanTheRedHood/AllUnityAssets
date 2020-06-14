using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestory : MonoBehaviour
{
    public float timeToDestory;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeToDestory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
