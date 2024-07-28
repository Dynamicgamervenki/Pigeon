using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using static Cinemachine.DocumentationSortingAttribute;

public class UiManager : MonoBehaviour
{
    CameraController cameraController;
    public GameObject pauseMenu;
    public GameObject gameLost;
    public GameObject gameWon;

    public bool isPaused;
    public bool game_Lost = false;
    public bool game_Won = false;

    public static UiManager instance;


    private void Awake()
    {
        // Ensure there is only one instance of ScoreManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        cameraController = FindObjectOfType<CameraController>();
        pauseMenu.gameObject.SetActive(false);
    }

    public void StartGame()
    {
   
        Time.timeScale = 1;
        AudioManager.Instance.PlaySFX("GameStart", 1.0f);
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseMenu.gameObject.activeSelf)
                UnPauseMenu();
            else
                PauseMenu();
        }

        if(isPaused || game_Lost || game_Won)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        if(game_Lost)
        {
            int id = SceneManager.GetActiveScene().buildIndex;
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(id);
            }
        }
        if(game_Won)
        {
            int id = SceneManager.GetActiveScene().buildIndex;
            int loadId = 0;
            if (id == 1)
                 loadId = 0;
            else if (id == 2)
                loadId = 1;
            if (Input.GetKeyDown(KeyCode.N))
            {
                SceneManager.LoadScene(loadId);
                AudioManager.Instance.PlayMusic("Theme");
            }
        }

    }

    public void LoadScene(int id)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(id);
    }

    public void PauseMenu()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseMenu.gameObject.SetActive(true);
    }

    public void UnPauseMenu() 
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
    }

    public void GameLost()
    {
        AudioManager.Instance.StopMusic("Theme");
        AudioManager.Instance.PlaySFX("GameLost",0.1f);
        game_Lost = true;
        gameLost.gameObject.SetActive(true);
    }

    public void GameWon()
    {
        game_Won = true;
        gameWon.gameObject.SetActive(true);
    }


}
