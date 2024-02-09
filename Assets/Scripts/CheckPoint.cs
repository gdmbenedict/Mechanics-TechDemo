using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        //checks if object is a Player
        if (other.gameObject.CompareTag("Player"))
        {
            //updates the spawn point to the connected spawn point
            other.GetComponent<Respawn>().UpdateSpawnPoint(spawnPoint);
        }
    }
}
