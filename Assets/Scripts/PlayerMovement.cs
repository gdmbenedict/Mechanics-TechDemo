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

    [SerializeField] private Vector3 moveDirection;

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 10f;

    [Header("Physics")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float groundDrag = 6f;
    [SerializeField] private float airDrag = 0f;

    [Header("Checks")]
    [SerializeField] private bool isGrounded;

    [Header("References")]
    [SerializeField] private GameManager gameManager;

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
        ControlDrag();
        GroundCheck();
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
    public void Move(InputAction.CallbackContext movementValue)
    {
        Vector2 movementVector = movementValue.ReadValue<Vector2>();
        //Debug.Log(movementVector);

        //transform.forward and transform.right modify the value to update with camera movement
        moveDirection = transform.forward * movementVector.y + transform.right * movementVector.x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded && !gameManager.isGamePaused())
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    //Functionality Methods

    /// <summary>
    /// Method that changes the acceleration of the player to move
    /// </summary>
    private void MovePlayer()
    {
        if (isGrounded)
        {
            rb.AddForce(moveDirection * moveSpeed * moveMult, ForceMode.Acceleration);
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
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.01f);
    }
    
}
