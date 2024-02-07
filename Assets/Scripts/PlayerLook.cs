using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private Vector2 camMovement;

    [SerializeField] private float SensX; //camera X sensativity
    [SerializeField] private float SensY; //camera Y sensativity

    [SerializeField] private float camMult = 0.01f;

    [SerializeField] private float xRotation; //rotation about the x axis (up - down
    [SerializeField] private float yRotation; //rotation about the y axis (right - left)

    //Method called on first frame
    private void Start()
    {
        
    }

    //Method called every frame
    private void Update()
    {
        MoveCamera();
    }

    //Input Handling

    void OnLook(InputValue lookValue)
    {
        camMovement = lookValue.Get<Vector2>();

        yRotation += camMovement.x * SensX * camMult;
        xRotation -= camMovement.y * SensY * camMult;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }

    //Functionality Methods

    private void MoveCamera()
    {
        //rotates camera 
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        
        //rotates player
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
