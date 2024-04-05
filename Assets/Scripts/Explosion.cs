using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        audioSource.Play();
        StartCoroutine(DestroyAfterBlast());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DestroyAfterBlast()
    {
        while (audioSource.isPlaying)
        {

            yield return null;
        }

        //Debug.Log("Destroying Explosion");
        Destroy(gameObject);
    }
}
