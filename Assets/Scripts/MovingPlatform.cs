using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Positions")]
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;

    [Header("Movement Modifiers")]
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float travelLength = 1.0f;
    private float distCovered;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distCovered = Mathf.PingPong(Time.time, travelLength/speed);
        transform.position = Vector3.Lerp(startPos.position, endPos.position, distCovered/travelLength);
    }

    //Trigger Method to lock player to the platform
    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
    }

    //Trigger Method to unlock player from the platform
    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }
}
