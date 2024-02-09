using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //enum to track state of the game.
    public enum GameState
    {
        paused,
        gamePlay
    }

    [Header("Game States")]
    [SerializeField] private GameState gameState;

    [Header("Menus")]
    [SerializeField] private GameObject pauseMenu;

    [Header("Info")]
    [ReadOnly(true)] private int targetsTotal;
    [ReadOnly(true)] private int targetsCollected;

    [SerializeField] TextMeshProUGUI targetsText;


    // Start is called before the first frame update
    void Awake()
    {
        gameState = GameState.gamePlay;
        pauseMenu.SetActive(false);

        //hiding cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        targetsTotal = 0;
        targetsCollected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        targetsText.text = targetsCollected.ToString() + "/" + targetsTotal.ToString() + " Targets Collected";
    }

    //Input Handling

    public void OnEscape(InputAction.CallbackContext context)
    {
        //Debug.Log("function called");
        Pause();
    }

    //Functionality Methods

    /// <summary>
    /// Method that toggles the pause state of the game.
    /// </summary>
    public void Pause()
    {
        if (gameState != GameState.paused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            gameState = GameState.paused;

            //Showing cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            gameState = GameState.gamePlay;

            //Hiding cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    /// <summary>
    /// Method that closes the application.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Method that reloads the current scene.
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Method that registers an instance of a target with the game manager.
    /// </summary>
    public void RegisterTarget()
    {
        targetsTotal++;
    }

    /// <summary>
    /// Method that registers that a target was collected.
    /// </summary>
    public void CollectTarget()
    {
        targetsCollected++;
    }

    //Accessor functions

    /// <summary>
    /// Method that returns if the game is paused
    /// </summary>
    /// <returns></returns>
    public bool isGamePaused(){
        if(gameState == GameState.paused)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
