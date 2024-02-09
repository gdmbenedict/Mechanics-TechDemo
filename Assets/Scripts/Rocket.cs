using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [Header("Grenade Variables")]
    [SerializeField] private float launchForce = 15f;
    [SerializeField] private float propulsionForce = 3f;
    [SerializeField] private float explosionForce = 15f;
    [SerializeField] private float explosionRadius = 3f;

    [Header("References")]
    [ReadOnly(true)] private Rigidbody rb;
    [SerializeField] private Transform explosionPoint;

    private Collider[] hitColliders;
    

    // Start is called before the first frame update
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        //prefab considers wrong direction forward
        rb.AddForce(-gameObject.transform.forward * launchForce, ForceMode.Impulse);
        
        //destroy the game object after 3 seconds
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        //prefab considers wrong direction forward
        rb.AddForce(-gameObject.transform.forward * propulsionForce, ForceMode.Acceleration);
    }

    //function called when target is hit.
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Collision Detected");
            Explode();
        }
    }

    /// <summary>
    /// Function that simulates explosive force and destroys the object.
    /// </summary>
    private void Explode()
    {
        //get colliders in explosion radius
        hitColliders = Physics.OverlapSphere(explosionPoint.position, explosionRadius);

        //step through affected objects
        foreach (Collider hitcol in hitColliders)
        {
            if (hitcol.GetComponent<Rigidbody>() != null)
            {
                hitcol.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, explosionPoint.position, explosionRadius, 1f, ForceMode.Impulse);
            }
            
        }

        Destroy(gameObject);
    }
}
