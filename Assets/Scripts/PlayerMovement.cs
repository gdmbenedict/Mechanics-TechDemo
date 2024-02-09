using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement: MonoBehaviour
{
    [Header("Player Variables")]
    [SerializeField] float playerHeight = 2f;

    [Header("Movement Variables")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float moveMult = 10f;
    [SerializeField] private float airMoveMult = 0.5f;

    [ReadOnly(true)] private Vector3 moveDirection;
    [ReadOnly(true)] private Vector3 slopeMoveDirection;

    [SerializeField] private Vector2 inputVector;

    [Header("Jumping")]
    //[SerializeField] private float jumpForce = 10f;
    [SerializeField] private float jumpHeight = 4f;

    [Header("Physics")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float groundDrag = 6f;
    [SerializeField] private float airDrag = 0f;

    [Header("Checks")]
    [ReadOnly(true)] public bool isGrounded;
    [ReadOnly(true)] public bool onSlope;

    //raycast for slope check
    private RaycastHit slopeHit;

    [Header("References")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] Transform orientation;

    //called on first frame regardless of script being enabled
    private void Awake()
    {
        //getting game manager from the scene
        gameManager = FindObjectOfType<GameManager>();   
    }

    /*
     * Update function that is called every frame
     */
    private void Update()
    {
        //checking floor
        GroundCheck();
        SlopeCheck();

        MoveDirection();
        SlopeMoveDirection();
        ControlDrag();
    }

    /*
     * Update function that happens in a fixed timeframe (for physics calculations)
     */
    private void FixedUpdate()
    {
        MovePlayer();
    }

    //Input Handling

    /// <summary>
    /// This function is called on "Move" input in the "PlayerControls" Input Action. It updates the values of the Vector3 moveDirection
    /// </summary>
    /// <param name="movementValue">InputValue of the "Move" input action (extracted as Vector2)</param>
    public void MoveInput(InputAction.CallbackContext movementValue)
    {
        inputVector = movementValue.ReadValue<Vector2>();
        //Debug.Log(inputVector);   
    }

    private void MoveDirection()
    {
        //transform.forward and transform.right modify the value to update with camera movement
        moveDirection = orientation.forward * inputVector.y + orientation.right * inputVector.x;
    }

    private void SlopeMoveDirection()
    {
        //calculates movement change over plane
        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded && !gameManager.isGamePaused() && context.performed)
        {
            //reset Y velocity to fix small jump bug
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            //rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            float jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            rb.AddForce(transform.up * jumpVelocity, ForceMode.VelocityChange);
        }
    }

    //Functionality Methods

    /// <summary>
    /// Method that changes the acceleration of the player to move
    /// </summary>
    private void MovePlayer()
    {
        //change how movement works based on environment
        if (isGrounded && !onSlope)
        {
            rb.AddForce(moveDirection * moveSpeed * moveMult, ForceMode.Acceleration);
        }
        else if (isGrounded && onSlope)
        {
            rb.AddForce(slopeMoveDirection * moveSpeed * moveMult, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(moveDirection * moveSpeed * moveMult * airMoveMult, ForceMode.Acceleration);
        }
        
        //Debug.Log(rb.velocity);
    }

    private void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    /// <summary>
    /// Method that checks if the player is  touching (or near enough) to the ground
    /// </summary>
    private void GroundCheck()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.1f);
    }

    private void SlopeCheck()
    {
        //check for floor
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if(slopeHit.normal != Vector3.up)
            {
                onSlope = true;
            }
            else
            {
                onSlope = false;
            }
        }
    }
    
}
