using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController
{
    public static float GetAngle(Vector2 lookDirection)
    {
        return lookDirection.normalized.x * 90 + (lookDirection.normalized.y > 0 ? 0 : 180);
    }
}
