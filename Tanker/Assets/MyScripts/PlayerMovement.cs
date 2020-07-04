using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;

    // Not present in all player prefab. Fail safe code required
    private Animator animator;

    private Rigidbody2D rigidbody2d;

    public Vector2 lookDirection = new Vector2(0, 1);

    public bool isDead = false;
    public float speed = 4.0f;

    // public Joystick joystick;

    // Start is called before the first frame update
    void Start()
    {

        this.rigidbody2d = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isDead)
        {
            return;
        }

         horizontal = InputController.instance.horizontal;
        //horizontal = Input.GetAxis("Horizontal");
         vertical = InputController.instance.vertical; //
        //vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            vertical = 0f;
        }
        else
        {
            horizontal = 0f;
        }

        if (!Mathf.Approximately(horizontal, 0.0f) || !Mathf.Approximately(vertical, 0.0f))
        {
            lookDirection.Set(horizontal, vertical);
            // Debug.Log("Look Direction: " + this.lookDirection);
            lookDirection.Normalize();
            // Debug.Log("Look Direction normalized: " + this.lookDirection);

            if (this.animator != null)
            {
                this.animator.SetBool("IsMoving", true);
            }
        } else
        {
            if (this.animator != null)
            {
                this.animator.SetBool("IsMoving", false);
            }
        }
    }

    void FixedUpdate()
    {
        if (this.isDead)
        {
            return;
        }

        //Vector2 direction = Vector2.up * this.vertical + Vector2.right * this.horizontal;

        //this.transform.Rotate(direction);

        //this.updateLookDirection();

        if (!Mathf.Approximately(horizontal, 0.0f) || !Mathf.Approximately(vertical, 0.0f))
        {
            //float angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, getAngle()));
            this.rigidbody2d.rotation = getAngle();
            this.lookDirection.Set(horizontal, vertical);

            // transform.localScale = new Vector3(getSign(this.horizontal), getSign(this.vertical), 0);
        }


        Vector2 position = transform.position;

        // Time.deltaTime. deltaTime, contained inside Time, is a variable that Unity fills with the time
        // it takes for a frame to be rendered. Your character now runs at the same speed, regardless
        // of the number of frames rendered by our game. It’s now “frame independent”.
        float steps = this.speed * Time.deltaTime;
        position.x += horizontal * steps;
        position.y += vertical * steps;

        //position = position + this.lookDirection * steps;

        this.rigidbody2d.MovePosition(position);
        //this.rigidbody2d.MoveRotation(Quaternion.Euler(this.lookDirection.x, 0, this.lookDirection.y));
    }


    private int updateLookDirection()
    {
        
        this.lookDirection = Mathf.Abs(horizontal) > Mathf.Abs(vertical) ? new Vector2(getSign(horizontal), 0): new Vector2(0, getSign(vertical));

        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            return getSign(horizontal) * 90;
        }

        if (getSign(vertical) < 0)
        {
            return 180;
        }

        return 0;
        
    }

    private int getSign(float num)
    {
        if (Mathf.Approximately(num, 0.0f))
        {
            return 0;
        }

        return num < 0 ? -1 : 1;
    }

    private int getAngle()
    {
        return getSign(this.horizontal) * 90 + (this.vertical > 0 ? 0 : 180); 
    }
}
