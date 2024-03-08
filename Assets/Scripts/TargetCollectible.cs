using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollectible : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private GameManager gameManager;

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

    //function that collects collectable and destroys the object.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Enemy")
        {

        }
        gameManager.CollectTarget();
        Destroy(gameObject);
    }




}
