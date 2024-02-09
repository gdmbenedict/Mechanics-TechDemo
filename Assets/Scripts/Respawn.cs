using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform initSpawnPoint;
    [ReadOnly(true)] private Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = initSpawnPoint;
    }

    public void UpdateSpawnPoint(Transform newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

    public void RespawnObject()
    {
        gameObject.transform.position = spawnPoint.position;
    }
}
