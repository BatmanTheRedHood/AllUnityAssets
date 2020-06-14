using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfView : MonoBehaviour
{
    public Transform player;

    bool m_IsPlayerInRange;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    void Update()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;

            // In Unity, it’s possible to check whether there are any Colliders along the path of a line starting from a point.
            // This line starting from a specific point is called a Ray.Checking for Colliders along this Ray is called a Raycast.
            // Your Ray needs an origin and a direction.
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            // Raycast method sets its data to information about whatever the Raycast hit.
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {

                }
            }
        }
    }
}
