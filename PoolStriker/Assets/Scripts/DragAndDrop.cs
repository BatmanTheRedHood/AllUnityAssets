using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private float dist;
    private bool dragging = false;
    private Vector3 offset;
    private Transform toDrag;
    private Collider col;
    
    private float distance;
    private Vector3 startDist;

    // Start is called before the first frame update
    void Start()
    {
        this.col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                distance = Vector3.Distance(transform.position, Camera.main.transform.position);
                dragging = true;
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                Vector3 rayPoint = ray.GetPoint(distance);
                startDist = transform.position - rayPoint;
                // Collider touchedCollider = Physics2D.OverlapPoint(touchPosition);
                // if (touchedCollider == this.col)
                // {
                //    this.moveAllowed = true;
                // }
            }

            if (dragging && (touch.phase == TouchPhase.Moved))
            {
                // Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                // transform.position = new Vector3(touchPosition.x, transform.position.y, touchPosition.z);

                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                Vector3 rayPoint = ray.GetPoint(distance);
                transform.position = new Vector3(rayPoint.x + startDist.x, transform.position.y, rayPoint.z + startDist.z);
            }

            if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
            {
                this.dragging = false;
            }
        }
    }
}
