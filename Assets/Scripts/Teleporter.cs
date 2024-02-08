using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform targetPosition;
    [SerializeField] private TeleportController teleportController;

    public void OnTriggerEnter(Collider other)
    {
        if (!teleportController.IsTravellerInRegistry(other.gameObject))
        {
            teleportController.AddToRegistry(other.gameObject);

            //get position by which teleport needs to be offset
            float heightOffset = other.gameObject.transform.position.y - gameObject.transform.position.y;

            other.gameObject.transform.position = targetPosition.position + new Vector3(0f, heightOffset, 0f);
        }
        
        
    }
}
