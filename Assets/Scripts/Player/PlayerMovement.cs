using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float walkSpeed = 7f;
    [SerializeField] float sprintSpeed = 10f;

    [Header("Jump")]
    [SerializeField] float jumpForce = 12f;
    [SerializeField] float jumpCooldown = 0.25f;
    [SerializeField] float airMultiplier = 0.4f;

    [Header("Ground Check")]
    [SerializeField] float groundDrag = 5f;
    [SerializeField] float groundDistance = 0.4f;

    [Header("What is ground layer")]
    [SerializeField] LayerMask isGround;

    [Header("Slope Handling")]
    [SerializeField] float maxSlopeAngle;

    //rest variables
    bool isGrounded = true;
    bool readyToJump = true;
    bool exitingSlope;

    float moveSpeed = 0;
    float horizontalInput;
    float verticalInput;

    Rigidbody playerRb;
    Transform orientation;
    AudioSource audioSource;
    
    RaycastHit slopeHit;
    Vector3 moveDirectrion;

    void Awake()
    {
        orientation = FindObjectOfType<Orientation>().transform;
        playerRb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        playerRb.freezeRotation = true;
        moveSpeed = walkSpeed;
        audioSource.Pause();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void Update()
    {
        GroundCheck();
        MyInput();
        SpeedControl();
        GroundDragHandeler();
    }

    void MovePlayer()
    {
        moveDirectrion = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        playerRb.useGravity = !OnSlope();

        if (OnSlope() && !exitingSlope)
        {
            playerRb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);
        }

        if (isGrounded)
        {
            playerRb.AddForce(10f * moveSpeed * moveDirectrion.normalized, ForceMode.Force);
        }
        else
        {
            playerRb.AddForce(10f * moveSpeed * moveDirectrion.normalized * airMultiplier, ForceMode.Force);
        }
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(orientation.position, groundDistance, isGround);
    }

    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space) && readyToJump && isGrounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(JumpReset), jumpCooldown);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }

        if (isGrounded && (horizontalInput != 0 || verticalInput != 0))
        {
            audioSource.UnPause();
        }
        else
        {
            audioSource.Pause();
        }
    }

    void SpeedControl()
    {
        if (OnSlope() && !exitingSlope)
        {
            if (playerRb.velocity.magnitude > moveSpeed)
            {
                playerRb.velocity = playerRb.velocity.normalized * moveSpeed;
            }
        }
        else
        {
            Vector3 flatVelocity = new (playerRb.velocity.x, 0f, playerRb.velocity.z);

            if (flatVelocity.magnitude > moveSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
                playerRb.velocity = new(limitedVelocity.x, playerRb.velocity.y, limitedVelocity.z);
            }
        }
    }

    void GroundDragHandeler()
    {
        if (isGrounded)
        {
            playerRb.drag = groundDrag;
        }
        else
        {
            playerRb.drag = 0;
        }
    } 

    void Jump()
    {
        exitingSlope = true;

        playerRb.velocity = new(playerRb.velocity.x, 0f, playerRb.velocity.z);
        playerRb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void JumpReset()
    {
        readyToJump = true;
        exitingSlope = false;
    }

    bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, groundDistance))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirectrion, slopeHit.normal).normalized;
    }
}
