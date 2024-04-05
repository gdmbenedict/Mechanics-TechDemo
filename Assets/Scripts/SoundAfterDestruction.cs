using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAfterDestruction : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        audioSource.clip = sound;
        audioSource.Play();

        StartCoroutine(DestroyAfterSound());
    }

    private IEnumerator DestroyAfterSound()
    {
        while (audioSource.isPlaying)
        {

            yield return null;
        }

        //Debug.Log("Destroying Explosion");
        Destroy(gameObject);
    }
}
