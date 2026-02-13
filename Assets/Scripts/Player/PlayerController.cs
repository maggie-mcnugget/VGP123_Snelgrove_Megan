using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    //public GameObject groundCheckTransform;
    [Header("Ground Check Settings")]
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.02f;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Powerup Settings")]
    public float initialPowerupDuration = 5f;
    public float powerupJumpForce = 20f;

    private float currentPowerupDuration = 0f;
    private float initialJumpForce = 7f;
    private Coroutine jumpforceCoroutine = null;

    public void JumpForceChange()
    {
        if (jumpforceCoroutine != null)
        {
            StopCoroutine(jumpforceCoroutine);
            jumpforceCoroutine = null;
            jumpForce = 7f;
        }
    }

    IEnumerator JumpForceChangeCoroutine()
    {
        currentPowerupDuration = initialPowerupDuration + currentPowerupDuration;
        jumpForce = powerupJumpForce;
        while (currentPowerupDuration > 0)
        {
            currentPowerupDuration -= Time.deltaTime;
            yield return null;
        }
        jumpForce = initialJumpForce;
        jumpforceCoroutine = null;
        currentPowerupDuration = 0;
    }

    private int _lives = 3;
    private int maxLives = 5;

    public int lives
    {
        get => _lives;
        set
        {
            if (value < 0)
            {
                //gameover logic here
                Debug.Log("Game Over!");
                return;
            }
            if (value > maxLives)
            {
                _lives = maxLives;
            }
            else
            {
                _lives = value;
            }
            Debug.Log("life pickup collected: Lives:" + _lives);
        }
    }

    //public void SetLives(int valueToAdd)
    //{
    //    lives += valueToAdd;
    //    if (lives > maxLives)
     //   {
     //       lives = maxLives;
     //   }
    //    if (lives < 0)
    //    {
   //         //gameover logic
   //     }

   // }

    private Rigidbody2D _rb;
    private Collider2D _collider;
    private SpriteRenderer _sr;
    private Animator _anim;
    private GroundCheck _groundCheck;

    private bool _isAttack = false;
    private bool _isGrounded = false;
    private bool _isJumpAttack = false;
    private bool _isFiring = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();

        _groundCheck = new GroundCheck(_collider, _rb, groundCheckRadius, groundLayer);

        initialJumpForce = jumpForce;

        ////initalize the ground check object here rather than in the inpsector for safety - only if we use a gameobject to get our foot position
        //if (groundCheckTransform == null)
        //{
        //    groundCheckTransform = new GameObject("GroundCheck");
        //    groundCheckTransform.transform.SetParent(transform);
        //    groundCheckTransform.transform.localPosition = Vector3.zero;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = _groundCheck.IsGrounded();

        //input handling
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        bool jumpInput = Input.GetButtonDown("Jump");
        bool attackInput = Input.GetButtonDown("Attack");
        bool fireInput = Input.GetButtonDown("Fire");

        if (!_isFiring)
        {
            Vector2 velocity = _rb.linearVelocity;
            velocity.x = horizontalInput * moveSpeed
;           _rb.linearVelocity = velocity;
        }
       


        if (horizontalInput != 0) SpriteFlip(horizontalInput);

        //jumping
        if (jumpInput && _isGrounded)
        {
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        if (fireInput && !_isFiring)
        {
            _isFiring = true;
        }

        if (!_isGrounded && attackInput)
        {
            _isJumpAttack = true;
        }
        else if (attackInput && _isGrounded)
        {
            _isAttack = true;
        }

        if (!attackInput)
        {
            _isAttack = false;
            _isJumpAttack = false;
        }
        //animation
        _anim.SetFloat("moveInput", Mathf.Abs(horizontalInput));
        _anim.SetFloat("yVel", _rb.linearVelocity.y);
        _anim.SetBool("isGrounded", _isGrounded);
        _anim.SetBool("isAttack", _isAttack);
        _anim.SetBool("isJumpAttack", _isJumpAttack);
        _anim.SetBool("Fire", _isFiring);
    }

    /// <summary>
    /// Sprite flipping based on horizontal input - this function should only be called when horizontal input is non-zero
    /// </summary>
    /// <param name="horizontalInput">The input received from Unity's input system</param>
    private void SpriteFlip(float horizontalInput) => _sr.flipX = (horizontalInput < 0);

    public void ResetFireAnimation()
    {
        _isFiring = false;
    }
    public void FireProjectile()
    {
        if (!_isFiring) return;

        Shoot shoot = GetComponent<Shoot>();
        if (shoot != null)
        {
            shoot.Fire();
        }
    }
}