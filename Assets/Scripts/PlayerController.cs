using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public float jumpF = 5;
    public int jumpCount = 2;

    private bool grounded = true;
    private Rigidbody rb;
    private int count;
    private int jump;

    void DoJump()
    {
        rb.AddForce(Vector3.up * jumpF, ForceMode.VelocityChange);
        jump = jump + 1;
        return;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        jump = 0;
        SetCountText();
        winText.text = "";
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded) {
                DoJump();
                grounded = false;
            }
            else if (jump < jumpCount)
            {
                DoJump();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            jump = 0;
        }
    }

void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winText.text = "You Win!";
        }
    }
}
