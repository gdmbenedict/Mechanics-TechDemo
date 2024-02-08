using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    private List<GameObject> teleportRegistry;
    [SerializeField] private float cooldown = 0.1f;
    private float timer = 0f;

    private void Start()
    {
        teleportRegistry = new List<GameObject>();
    }

    private void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        

        if (timer <= 0 && teleportRegistry.Count > 0)
        {
            teleportRegistry.Clear();
        }
    }

    public void AddToRegistry(GameObject traveller)
    {
        teleportRegistry.Add(traveller);
        timer = cooldown;
    }

    public bool IsTravellerInRegistry(GameObject traveller)
    {
        if (teleportRegistry.Contains(traveller))
        {
            return true;
        }
        return false;
    }
}
