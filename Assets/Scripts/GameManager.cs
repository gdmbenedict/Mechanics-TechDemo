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
    [SerializeField] private bool controls;
    [SerializeField] private bool features;

    [Header("Menus")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject featuresMenu;

    [Header("Info")]
    [SerializeField] private GameObject enemyLegend;
    [SerializeField] TextMeshProUGUI targetsText;

    [ReadOnly(true)] private int targetsTotal;
    [ReadOnly(true)] private int targetsCollected;
    


    // Start is called before the first frame update
    void Awake()
    {
        gameState = GameState.gamePlay;
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(false);
        featuresMenu.SetActive(false);
        enemyLegend.SetActive(true);

        controls = false;
        features = false;

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
        if (controls)
        {
            ToggleControls();
        }
        else if (features)
        {
            ToggleFeatures();
        }
        else
        {
            //Debug.Log("function called");
            Pause();
        }
    }

    //Functionality Methods

    /// <summary>
    /// Method that toggles the pause state of the game.
    /// </summary>
    public void Pause()
    {
        if (gameState != GameState.paused)
        {
            enemyLegend.SetActive(false);

            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            gameState = GameState.paused;

            //Showing cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            enemyLegend.SetActive(true);

            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            gameState = GameState.gamePlay;

            //Hiding cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    /// <summary>
    /// Function that toggles the controls menu
    /// </summary>
    public void ToggleControls()
    {
        if (gameState == GameState.paused)
        {
            controls = !controls;

            controlsMenu.SetActive(controls);
            pauseMenu.SetActive(!controls);
        }
    }

    /// <summary>
    /// Function that toggles the features menu
    /// </summary>
    public void ToggleFeatures()
    {
        if (gameState == GameState.paused)
        {
            features = !features;

            featuresMenu.SetActive(features);
            pauseMenu.SetActive(!features);
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
