using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // ****** Mobile controls ******** //
    public static bool isScreenTap()
    {
         //return isMouseLeftClick();

        //*
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                // Vector2 touchPosition = touch.position;
                return true;
            }
        }

        return false;

        //*/
    }



    // ***** Desktop comntrol ***** //
    public static bool isMouseLeftClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }

        return false;
    }
}
