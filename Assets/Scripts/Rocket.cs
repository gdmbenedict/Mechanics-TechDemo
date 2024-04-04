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

    [Header("Thruster")]
    [SerializeField] private GameObject trailParticles;
    [SerializeField] private float thrusterStartDelay = 0.6f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip rocketLoop;

    [Header("Explosion")]
    [SerializeField] private GameObject explosion;
    
    // Start is called before the first frame update
    private void Awake()
    {
        trailParticles.SetActive(false);
        rb = gameObject.GetComponent<Rigidbody>();

        //prefab considers wrong direction forward
        rb.AddForce(-gameObject.transform.forward * launchForce, ForceMode.Impulse);

        //start playing sound after a delay.
        Invoke("StartThrusters", thrusterStartDelay);

        //destroy the game object after 3 seconds
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(new Ray(gameObject.transform.position, rb.velocity * Time.deltaTime), out RaycastHit hit))
        {
            if (hit.collider.tag != "Player" && hit.collider.tag != "Rocket")
            {
                Debug.Log(hit.collider.gameObject.ToString());
                Explode();
            }
        }

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

        Debug.Log("Calling explosion Instantiation");
        Instantiate(explosion, explosionPoint.position, explosionPoint.rotation);

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

    private void StartThrusters()
    {
        trailParticles.SetActive(true);

        audioSource.loop = true;
        audioSource.clip = rocketLoop;
        audioSource.Play();
    }
}
