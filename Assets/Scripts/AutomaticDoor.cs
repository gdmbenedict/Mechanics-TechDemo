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

    [Header("Doors")]
    [SerializeField] private GameObject positiveDoor;
    [SerializeField] private GameObject negativeDoor;

    private bool objectDetected = false;

    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        //opening door
        if (objectDetected)
        {
            //lerping from current position to open
            positiveDoor.transform.position = Vector3.Lerp(positiveDoor.transform.position, positiveOpenPos.position, getPositiveIncrement());
            negativeDoor.transform.position = Vector3.Lerp(negativeDoor.transform.position, negativeOpenPos.position, getNegativeIncrement());
        }
        //closing door
        else
        {
            //lerping from current position to closed
            positiveDoor.transform.position = Vector3.Lerp(positiveDoor.transform.position, positiveClosedPos.position, getPositiveIncrement());
            negativeDoor.transform.position = Vector3.Lerp(negativeDoor.transform.position, negativeClosedPos.position, getNegativeIncrement());
        }
    }

    //Method to detect object arriving
    private void OnTriggerEnter(Collider other)
    {
        objectDetected = true;
    }

    //method to detect object leaving
    private void OnTriggerExit(Collider other)
    {
        objectDetected = false;
    }

    //gets the the increment for movement for the positive door
    private float getPositiveIncrement()
    {
        return Vector3.Distance(positiveClosedPos.position, positiveOpenPos.position) / doorOpenTime * Time.deltaTime;
    }

    //gets the increment for movement for the negative door
    private float getNegativeIncrement()
    {
        return Vector3.Distance(negativeClosedPos.position, negativeOpenPos.position) / doorOpenTime * Time.deltaTime;
    }
}
