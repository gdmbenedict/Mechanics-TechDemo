using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RocketLauncher : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private Transform launchPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //sets initial conditions for the rocket launcher
    private void SetInitialReferences()
    {

    }

    //function that instantiates a rocket and launches it
    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Instantiate(rocketPrefab, launchPos.position, launchPos.rotation);
        }
    }


}
