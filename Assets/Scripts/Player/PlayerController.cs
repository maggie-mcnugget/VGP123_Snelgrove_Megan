using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour


{
   // public GameObject GroundCheckTransform;
    public LayerMask groundlayer;
    

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float GroundCheckRadius = 0.2f;
    
    private Rigidbody2D _rb;
    private Collider2D _collider;
    private bool _isGrounded = false;
    private Vector2 groundCheckPos => CalculateGroundCheck();

    private Vector2 CalculateGroundCheck()
    {
        Bounds bounds = _collider.bounds;
        return new Vector2(bounds.center.x, bounds.min.y);
    }

// Start is called once before the first execution of Update after the MonoBehaviour is created
void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

       // if (GroundCheckTransform == null)
           // (
               // GroundCheckTransform = new GameObject ("GroundCheck")
               // GroundCheckTransform.transform.SetParent(transform);
        //GroundCheckTransform.transform.localPosition = Vector3.zero;
       // )
    }

    // Update is called once per frame
    void Update()
    {

        _isGrounded = Physics2D.OverlapCircle(groundCheckPos, GroundCheckRadius, groundlayer);

        float horizontalInput = Input.GetAxis("Horizontal");
        bool jumpInput = Input.GetButtonDown("Jump");

        Vector2 velocity = _rb.linearVelocity;
        velocity.x = horizontalInput * moveSpeed;
        _rb.linearVelocity = velocity;

        if (jumpInput && _isGrounded)
        {
            _rb.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
        }
    }
}
