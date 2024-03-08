using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionTrigger : MonoBehaviour
{
    [SerializeField] private BasicEnemy enemy;

    private void Awake()
    {
        //grab enemy component
        enemy = GetComponentInParent<BasicEnemy>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemy.DetectPlayer(other.transform);
        }
    }
}
