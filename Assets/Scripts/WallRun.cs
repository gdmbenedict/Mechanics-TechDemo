using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallRun : MonoBehaviour
{
    [SerializeField] private Transform orientation;

    [Header("Detection")]
    [SerializeField] private float wallDist = 0.6f;
    [SerializeField] private float minimumJumpHeight = 1.0f;

    private bool wallLeft = false;
    private bool wallRight = false;

    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;

    private bool isWallRunning;

    [Header("Wall Running Variables")]
    [SerializeField] private float wallRunGravity = 2.0f;
    [SerializeField] private float wallRunJumpHeight = 4f;

    [Header("Object References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //getting game manager from the scene
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //check for walls
        CheckWall();

        //check if touching the ground
        if (CanWallRun())
        {
            if (wallLeft)
            {
                StartWallRun();
                Debug.Log("wall running on left");
            }

            else if (wallRight)
            {
                StartWallRun();
                Debug.Log("wall running on right");
            }
            else
            {
                StopWallRun();
            }
        }
        else
        {
            StopWallRun();
        }
    }

    /// <summary>
    /// Method that checks for wall on either side of the player
    /// </summary>
    void CheckWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDist);
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallDist);
    }

    /// <summary>
    /// Method that checks if the player is not touching the ground
    /// </summary>
    /// <returns>inverse of if the player is touching the ground</returns>
    private bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);
    }

    private void StartWallRun()
    {
        isWallRunning = true;
        rb.useGravity = false;

        //applying custom gravity to player
        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);
    }

    private void StopWallRun()
    {
        isWallRunning = false;
        rb.useGravity = true;
    }

    public void WallJump(InputAction.CallbackContext context)
    {
        if (isWallRunning && !gameManager.isGamePaused() && context.performed)
        {
            if (wallLeft)
            {
                Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal;

                //reset Y velocity to fix small jump bug
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

                //calculate jump velocity and apply it to normalized wall jump vector
                float jumpVelocity = Mathf.Sqrt(wallRunJumpHeight * -2f * Physics.gravity.y);
                rb.AddForce(wallRunJumpDirection.normalized * jumpVelocity, ForceMode.VelocityChange);
            }
            else if (wallRight)
            {
                Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal;

                //reset Y velocity to fix small jump bug
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

                //calculate jump velocity and apply it to normalized wall jump vector
                float jumpVelocity = Mathf.Sqrt(wallRunJumpHeight * -2f * Physics.gravity.y);
                rb.AddForce(wallRunJumpDirection.normalized * jumpVelocity, ForceMode.VelocityChange);
            }
        }
    }
}
