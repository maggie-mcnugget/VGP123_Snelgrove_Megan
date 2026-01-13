using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizonal");
        bool jumpInput = Input.GetButtonDown("Jump");

        Vector2 velocity = rb.linearVelocity;
        velocity.x = horizontalInput + moveSpeed;
        rb.linearVelocity = velocity;

        if (jumpInput )
        {
            rb.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
        }
    }
}
