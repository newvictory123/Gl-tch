using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Movement")]
    public float walkSpeed;
    public float runSpeed;
    public float moveSpeed;
    public Vector3 moveDirection;
    public float jumpForce;
    public bool canJump;
    private float airDrag;
    private float slideDrag;

    [Header("Inputs")]
    public float horizontalInput;
    public float verticalInput;
    public bool sprintInput;
    public bool jumpInput;
    public bool slideInput;
    public bool slideReset;

    [Header("Ground control")]
    public float groundDrag;
    public float playerHeight;
    public LayerMask Ground;
    public bool isGrounded;

    [Header("Slope")]
    public float maxSlopeAngle;
    public RaycastHit slopeHit;

    [Header("Slide")]
    public float maxSlideTime;
    public float slideTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerHeight = transform.localScale.y;
        airDrag = groundDrag / 2f;
        slideDrag = groundDrag - airDrag/2f;
        slideTime = maxSlideTime;
        slideReset = true;
    }

    private void Update()
    {
        GetInput();
        GroundCheck();
        SpeedControl();
        Sprinting();
        Sliding();

        if (isGrounded && slideInput)
        {
            rb.drag = slideDrag;
            canJump = true;
        }
        else if (isGrounded && !slideInput)
        {
            rb.drag = groundDrag;
            canJump = true;
        }
        else
        {
            rb.drag = airDrag;
            canJump = false;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        Jumping();
        GravityForce();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        jumpInput = Input.GetButton("Jump");
        KeyCode shift = KeyCode.LeftShift;
        sprintInput = Input.GetKey(shift);
        KeyCode crouch = KeyCode.LeftControl;
        if (slideReset)
        {
            slideInput = Input.GetKey(crouch);
        }
        if (Input.GetKeyUp(crouch))
        {
            slideReset = true;
            slideTime = maxSlideTime;
        }
    }

    private void MovePlayer()
    {
        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        if (SlopeCheck())
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection * moveSpeed, ForceMode.Force);
        }
    }

    private void GravityForce()
    {
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * jumpForce, ForceMode.Acceleration);
        }
    }

    private void SpeedControl()
    {
        Vector3 Vel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (Vel.magnitude > moveSpeed)
        {
            rb.velocity = new Vector3(Vel.normalized.x * moveSpeed, rb.velocity.y, Vel.normalized.z * moveSpeed);
        }

        if (SlopeCheck())
        {
            if (rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        } 
    }
    private bool GroundCheck()
    {
        return isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight + 0.1f, Ground);
    }

    private void Jumping()
    {
        if (jumpInput && canJump)
        {
            Jump();
            canJump = false;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void Sprinting()
    {
        if (sprintInput)
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }
    }

    private void Sliding()
    {
        if (slideInput && isGrounded && slideTime > 0) 
        {
            slideTime -= Time.deltaTime;
            transform.localScale = new Vector3(transform.localScale.x, playerHeight / 2, transform.localScale.z);
            rb.AddForce(Vector3.down * 0.5f, ForceMode.Impulse);
        }
        if (slideInput && !isGrounded && slideTime > 0)
        {
            slideTime -= Time.deltaTime;
            transform.localScale = new Vector3(transform.localScale.x, playerHeight / 2, transform.localScale.z);
        }
        if (slideTime <= 0)
        {
            slideInput = false;
            slideReset = false;
            transform.localScale = new Vector3(transform.localScale.x, playerHeight, transform.localScale.z);
        }
        if (!slideInput)
        {
            transform.localScale = new Vector3(transform.localScale.x, playerHeight, transform.localScale.z);
        }
    }

    private bool SlopeCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

}
