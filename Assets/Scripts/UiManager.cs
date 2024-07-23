using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class UiManager : MonoBehaviour
{
    CameraController cameraController;
    public GameObject options;
    public GameObject pauseMenu;

    public bool isPaused;

    private void Awake()
    {
        cameraController = FindObjectOfType<CameraController>();
        pauseMenu.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
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
    }

    public void PauseMenu()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseMenu.gameObject.SetActive(true);
    }

    public void UnPauseMenu() 
    {
        pauseMenu.gameObject.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }


}
