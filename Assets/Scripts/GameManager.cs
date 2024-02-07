using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

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

    // Start is called before the first frame update
    void Awake()
    {
        gameState = GameState.gamePlay;
        pauseMenu.SetActive(false);

        //hiding cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Input Handling

    void OnEscape()
    {
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

    //Accessor functions

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
