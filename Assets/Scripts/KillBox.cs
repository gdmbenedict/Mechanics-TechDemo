using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    //function that calls the player's respawn on entering
    private void OnTriggerEnter(Collider other)
    {
        //checks if object is a Player
        if (other.gameObject.CompareTag("Player"))
        {
            //updates the spawn point to the connected spawn point
            other.GetComponent<Respawn>().RespawnObject();
        }
    }
}
