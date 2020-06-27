using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController instance;

    [HideInInspector]
    public float horizontal;

    [HideInInspector]
    public float vertical;

    public Joystick joystick;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        this.horizontal = joystick.Horizontal;
        //horizontal = Input.GetAxis("Horizontal");
        this.vertical = joystick.Vertical; //
    }

    public static float GetAngle(Vector2 lookDirection)
    {
        return lookDirection.normalized.x * 90 + (lookDirection.normalized.y > 0 ? 0 : 180);
    }
}
