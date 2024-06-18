using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    InputManager inputManager;
    PlayerLocomotion playerLocomotion;
    CameraManager cameraManager;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        cameraManager = FindObjectOfType<CameraManager>();
    }
    private void Update()
    {
        inputManager.HandleAllInputs();
        playerLocomotion.HandleAllMovements();
    }

    private void FixedUpdate()
    {
        //playerLocomotion.HandleAll();
    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovemnts();
    }
}
