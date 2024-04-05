using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollectible : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private GameManager gameManager;

    [Header("Sound Effect")]
    [SerializeField] private GameObject soundAfterDestruction;
    [SerializeField] private AudioClip pickupSound;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.RegisterTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGamePaused())
        {
            //rotate the object
            gameObject.transform.Rotate(new Vector3(0f, 0f, 45f) * Time.deltaTime);
        }
    }

    //function that call on a collider entering the collectible
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Enemy")
        {
            Collect();
        }
        
    }

    /// <summary>
    /// Method that collects the collectible
    /// </summary>
    public void Collect()
    {
        //plays the pickup sound
        GameObject soundPlayer = Instantiate(soundAfterDestruction, gameObject.transform.position, gameObject.transform.rotation);
        soundPlayer.GetComponent<SoundAfterDestruction>().PlaySound(pickupSound);

        //informs game manager and self destructs
        gameManager.CollectTarget();
        Destroy(gameObject);
    }






}
