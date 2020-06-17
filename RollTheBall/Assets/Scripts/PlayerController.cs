using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;

    public float speed;
    public Text countText;
    public Text winText;

    void Awake()
    {
        Debug.Log("Awake called!");
    }

    // Start is called before the first frame update
    void Start()
    {
        this.count = 0;
        this.rb = GetComponent<Rigidbody>();

        this.winText.text = "";
        this.SetCountText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //When the Primitive collides with the walls, it will reverse direction
    private void OnTriggerEnter(Collider other)
    {
        // Destroy(other.gameObject);
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;

            this.SetCountText();
        }
    }

    // Physics Update code
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        this.rb.AddForce(movement * this.speed);
    }

    void SetCountText()
    {
        this.countText.text = "Count: " + this.count.ToString();

        if (this.count >= 8)
        {
            this.winText.text = "You won!";
        }
    }
}
