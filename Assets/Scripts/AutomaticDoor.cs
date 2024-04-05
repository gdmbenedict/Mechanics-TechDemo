using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AutomaticDoors : MonoBehaviour
{
    [Header("Door Opening Variables")]
    [SerializeField] private Transform positiveClosedPos;
    [SerializeField] private Transform positiveOpenPos;
    [SerializeField] private Transform negativeClosedPos;
    [SerializeField] private Transform negativeOpenPos;
    [SerializeField] private float doorOpenTime =1f;
    private Vector3 posVelocity = Vector3.zero;
    private Vector3 negVelocity = Vector3.zero;

    [Header("Doors")]
    [SerializeField] private GameObject positiveDoor;
    [SerializeField] private GameObject negativeDoor;

    private bool objectDetected = false;
    private bool lastDetectionState = false;

    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip openAudioClip;
    [SerializeField] private AudioClip closeAudioClip;

    // Start is called before the first frame update
    void Start()
    { 
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //opening door
        if (objectDetected)
        {
            //determine if sound needs to be played
            if (lastDetectionState != objectDetected)
            {
                audioSource.Stop();
                audioSource.clip = openAudioClip;
                audioSource.Play();
            }

            positiveDoor.transform.position = Vector3.SmoothDamp(positiveDoor.transform.position, positiveOpenPos.position, ref posVelocity, doorOpenTime);
            negativeDoor.transform.position = Vector3.SmoothDamp(negativeDoor.transform.position, negativeOpenPos.position, ref negVelocity, doorOpenTime);
        }
        //closing door
        else
        {
            //determine if sound needs to be played
            if (lastDetectionState != objectDetected)
            {
                audioSource.Stop();
                audioSource.clip = closeAudioClip;
                audioSource.Play();
            }

            //lerping from current position to closed
            positiveDoor.transform.position = Vector3.SmoothDamp(positiveDoor.transform.position, positiveClosedPos.position, ref posVelocity, doorOpenTime);
            negativeDoor.transform.position = Vector3.SmoothDamp(negativeDoor.transform.position, negativeClosedPos.position, ref negVelocity, doorOpenTime);
        }

        lastDetectionState = objectDetected;
    }

    //Method to detect object arriving
    private void OnTriggerStay(Collider other)
    {
        objectDetected = true;
    }

    //method to detect object leaving
    private void OnTriggerExit(Collider other)
    {
        objectDetected = false;
    }
}
