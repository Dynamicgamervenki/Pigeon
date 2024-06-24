//using System.Collections;
//using System.Collections.Generic;
//using UnityEditorInternal;
//using UnityEngine;
//using UnityEngine.Animations.Rigging;
//using UnityEngine.EventSystems;

//public class PlayerLocomotion : MonoBehaviour
//{
//    [Header("Movemnt")]
//    public float rotationSpeed = 10.0f;
//    public bool  player_Moving;

//    [Header("Tilt")]
//    public float TiltSpeed = 2.0f;
//    public float TiltAngle = 30.0f;

//    [Header("Flying")]
//    public float flyingSpeed = 5.0f;
//    public float flightForce = 5.0f;
//    public float fallingSpeed = 100.0f;

//    //[Header("Pooping")]
//    //public Transform PoopLaunchPoint;
//    //public GameObject Poop;
//    //public float PoopLaunchSpeed = 1.0f;

//    //public LineRenderer lineRenderer; // LineRenderer for trajectory
//    //public int trajectoryResolution = 30; // Number of points in trajectory
//    //public float timeStep = 0.1f; // Time step between points

//    private Rigidbody rb;
//    private Vector3 moveDirection;
//    private Transform camera;
//    InputManager inputManager;



//    private void Awake()
//    {
//        camera = Camera.main.transform;
//        inputManager = GetComponent<InputManager>();
//        rb = GetComponent<Rigidbody>();

//    }

//    public void HandleAllMovements()
//    {
//        HandleMovement();
//        HandleRotation();
//    }

//    private void HandleMovement()
//    {

//            moveDirection = camera.forward * Mathf.Abs(inputManager.verticalInput);
//            moveDirection += camera.right * inputManager.horizontalInput;
//            moveDirection.Normalize();
//            moveDirection.y = 0f;

//            rb.useGravity = false;
//            moveDirection *= flyingSpeed;



//        player_Moving = moveDirection.magnitude > 0;

//        rb.velocity = moveDirection;

//        if (!player_Moving)
//        {
//            rb.useGravity = true;
//            rb.AddForce(Vector3.down * fallingSpeed, ForceMode.Force);
//        }
//    }

//    private void HandleRotation()
//    {
//        Vector3 targetDirection = Vector3.zero;

//        // Calculate the target direction based on input
//        targetDirection = camera.right * inputManager.horizontalInput;
//        targetDirection.Normalize();
//        targetDirection.y = 0; // Keep the target direction on the horizontal plane

//        // If there's no input, maintain the current forward direction
//        if (targetDirection == Vector3.zero)
//        {
//            targetDirection = transform.forward;
//        }

//        // Calculate the target rotation based on the target direction
//        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

//        // Smoothly interpolate to the target rotation
//        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
//    }


//    Quaternion targetRotation;
//    public void HandleLeftTilt()
//    {
//        targetRotation = Quaternion.Euler(0, 0, TiltAngle);

//        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, TiltSpeed * Time.deltaTime);
       
//    }

//    public void HandleRightTilt()
//    {
//        targetRotation = Quaternion.Euler(0, 0, -TiltAngle);

//        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, TiltSpeed * Time.deltaTime);

//    }

//    public void HandleReverseTilt()
//    {
//        Quaternion targetRotation = Quaternion.Euler(0,0,0);
//        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, TiltSpeed * Time.deltaTime);
//    }


//    public void HandleFlight()
//    {
//        rb.AddForce(Vector3.up * flightForce * 100, ForceMode.Acceleration);
//    }
//}


