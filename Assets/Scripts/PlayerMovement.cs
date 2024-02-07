using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement: MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float moveMult = 10f;

    [SerializeField] private Vector3 moveDirection;

    [Header("Physics")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float drag = 6f;

    /*
     * Update function that is called every frame
     */
    private void Update()
    {
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
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        //Debug.Log(movementVector);

        //transform.forward and transform.right modify the value to update with camera movement
        moveDirection = transform.forward * movementVector.y + transform.right * movementVector.x;
    }

    //Functionality Methods

    private void MovePlayer()
    {
        rb.AddForce(moveDirection * moveSpeed * moveMult, ForceMode.Acceleration);
        //Debug.Log(rb.velocity);
    }

    private void ControlDrag()
    {
        rb.drag = drag;
    }

    
}
