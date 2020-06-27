using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rgbody;
    private Vector2 prevVelocity;

    [HideInInspector]
    public Vector2 lookDirection = new Vector2(0, -1);

    public float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.rgbody = GetComponent<Rigidbody2D>();
        this.rgbody.velocity = this.lookDirection * this.speed;
        this.rgbody.rotation = InputController.GetAngle(this.lookDirection);
        this.prevVelocity = this.rgbody.velocity;

        // this.InvokeRandom();
        StartCoroutine(changeDirection());
    }

    // Update is called once per frame
    void Update()
    {
        if( this.prevVelocity != this.rgbody.velocity)
        {
            this.rgbody.velocity = this.prevVelocity;
        }
    }
    

    private void InvokeRandom()
    {
        Invoke("changeDirection", 2f);
    }

    IEnumerator changeDirection()
    {
        //yield return new WaitForSeconds(1.5f);
        //this.rgbody.velocity = this.lookDirection * this.speed;

        yield return new WaitForSeconds(5f);

        while (true)
        {
            this.setRandomDirection();

            float randomInterval = Random.Range(0f, 10f);
            //Invoke("InvokeRandom", randomInterval);
            yield return new WaitForSeconds(randomInterval);
        }
    }

    private void setRandomDirection()
    {
        int random = Random.Range(0, 4); // 0: UP, 1: DOWN, 2: LEFT, 3: RIGHT

        switch(random)
        {
            case 0:
                this.lookDirection.Set(0, 1);
                break;
            case 1:
                this.lookDirection.Set(0, -1);
                break;
            case 2:
                this.lookDirection.Set(-1, 0);
                break;
            case 3:
                this.lookDirection.Set(1, 0);
                break;
        }

        // this.lookDirection.Normalize();
        this.rgbody.velocity = this.lookDirection * this.speed;
        this.rgbody.rotation = InputController.GetAngle(this.lookDirection);
        this.prevVelocity = this.rgbody.velocity;
    }
}
