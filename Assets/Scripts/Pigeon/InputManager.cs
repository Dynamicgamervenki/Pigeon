using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Pigeon pigeon;

    [Header("Inputs")]
    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;

    public float moveAmount;
    public float horizontalInput;
    public float verticalInput;

    public bool LeftTiltInput;
    public bool RightTiltInput;
    public bool flightInput;
    public bool PoopInput;

    AniamtorManager aniamtorManager;
    PlayerLocomotion playerLocomotion;


    private void Awake()
    {
        aniamtorManager = GetComponent<AniamtorManager>(); 
        playerLocomotion = GetComponent<PlayerLocomotion>();    
    }

    private void OnEnable()
    {
        if(pigeon == null)
        {
            pigeon = new Pigeon();

            pigeon.Movement.Move.performed += ctx =>
            {
                movementInput = ctx.ReadValue<Vector2>();
            };
            pigeon.Movement.Move.canceled += ctx =>
            {
                movementInput = Vector3.zero;
            };

            pigeon.Movement.Camera.performed += ctx =>
            {
                cameraInput = ctx.ReadValue<Vector2>();
            };

            pigeon.Movement.LeftTilt.performed += ctx =>
            {
                LeftTiltInput = true;
            };
            pigeon.Movement.LeftTilt.canceled += ctx =>
            {
                LeftTiltInput = false;
            };


            pigeon.Movement.RightTilt.performed += ctx =>
            {
                RightTiltInput = true;
            };
            pigeon.Movement.RightTilt.canceled += ctx =>
            {
                RightTiltInput = false;
            };

            pigeon.Movement.Flight.performed += ctx =>
            {
                flightInput = true;
            };
            pigeon.Movement.Flight.canceled += ctx =>
            {
                flightInput = false;
            };

            pigeon.Movement.Poop.performed += ctx =>
            {
                PoopInput = true;   
            };
            pigeon.Movement.Poop.canceled += ctx =>
            {
                PoopInput = false;
            };
        }
        pigeon.Enable();
    }

    private void OnDisable()
    {
        pigeon.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleLeftTiltInput();
        HandleRightTiltInput();
        HandleFlightInput();
        HandlePoopInput();
    }

    private void HandleMovementInput()
    {
        horizontalInput = movementInput.x;
        verticalInput = movementInput.y;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        //moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        aniamtorManager.UpdateAnimatorValues(horizontalInput, verticalInput);
    }

    private void HandleLeftTiltInput()
    {
        if(LeftTiltInput)
        {
            playerLocomotion.HandleLeftTilt();
        }
        else
        {
            playerLocomotion.HandleReverseTilt();
        }
    }

    private void HandleRightTiltInput()
    {
        if (RightTiltInput)
        {
            playerLocomotion.HandleRightTilt();
        }
        else
        {
            playerLocomotion.HandleReverseTilt();
        }
    }

    private void HandleFlightInput()
    {
        if(flightInput)
        {
            playerLocomotion.HandleFlight();
        }
    }

    private void HandlePoopInput()
    {
        if(PoopInput)
        {
            PoopInput = false;
            playerLocomotion.HandlePoop();
        }
    }



}
