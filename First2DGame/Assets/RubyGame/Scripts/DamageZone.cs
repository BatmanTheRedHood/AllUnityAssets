using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        RubyController rubyController = other.GetComponent<RubyController>();

        if (rubyController != null)
        {
            rubyController.changeHealth(-1);
        }
    }
    */

    // This function is called every frame the Rigidbody is inside the Trigger instead of just once when it enters.
    private void OnTriggerStay2D(Collider2D other)
    {
        RubyController rubyController = other.GetComponent<RubyController>();

        if (rubyController != null)
        {
            rubyController.changeHealth(-1);
        }
    }
}
