using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CameraManager cameraManager;
    Animator animator;
    CharacterController characterController; // Reference to CharacterController component

    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float verticalSpeed = 2f; // Speed for vertical movement when space is pressed
    public GameObject poopPrefab; // Reference to the poop prefab
    public Transform poopSpawnPoint; // Reference to the poop spawn point
    public float poopForce = 10f; // Force to apply to the poop to make it fall

    public float tiltAngle = 30f; // Angle for tilting when Q or R key is pressed
    public float tiltSmoothness = 5f; // Speed of tilt interpolation
    private Quaternion initialRotation; // Store initial rotation for resetting tilt
    private Quaternion targetRotation; // Target rotation for tilting

    private void Awake()
    {
        cameraManager = FindObjectOfType<CameraManager>(); // Assuming CameraManager is a script in your scene
        animator = GetComponent<Animator>(); // Assuming Animator is attached to the same GameObject as this script
        characterController = GetComponent<CharacterController>(); // Get the CharacterController component
        initialRotation = transform.rotation; // Store initial rotation of the bird
        targetRotation = initialRotation; // Initialize target rotation to initial rotation
    }

    void Update()
    {
        // Get input
        float moveForward = Input.GetAxis("Vertical"); // W/S or Up/Down arrow keys

        // Move the pigeon forward using CharacterController
        if (moveForward != 0)
        {
            Vector3 moveDirection = transform.forward * moveForward * moveSpeed * Time.deltaTime;
            characterController.Move(moveDirection);
        }

        // Smoothly tilt the bird to the left when Q key is pressed
        if (Input.GetKey(KeyCode.Q))
        {
            targetRotation = Quaternion.Euler(initialRotation.eulerAngles + new Vector3(0, 0, tiltAngle));
        }
        // Smoothly tilt the bird to the right when E key is pressed
        else if (Input.GetKey(KeyCode.E))
        {
            targetRotation = Quaternion.Euler(initialRotation.eulerAngles + new Vector3(0, 0, -tiltAngle));
        }
        else
        {
            targetRotation = initialRotation; // Reset target rotation to initial rotation
        }

        // Interpolate towards the target rotation for smooth tilting
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, tiltSmoothness * Time.deltaTime);

        // Move the pigeon up when space key is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            characterController.Move(Vector3.up * verticalSpeed * Time.deltaTime);
        }

        // Move the pigeon down when S key is pressed
        if (Input.GetKey(KeyCode.S))
        {
            characterController.Move(Vector3.down * verticalSpeed * Time.deltaTime);
        }

        // Update animator parameters based on movement
        animator.SetFloat("Speed", (moveForward)); // Assuming "Speed" is a parameter in your animator

        // Make the pigeon poop when ctrl key is pressed
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            Poop();
        }

    }

    void Poop()
    {
        // Instantiate poop at the spawn point
        if (poopPrefab != null && poopSpawnPoint != null)
        {
            GameObject poop = Instantiate(poopPrefab, poopSpawnPoint.position, poopSpawnPoint.rotation);
            Rigidbody rb = poop.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = poop.AddComponent<Rigidbody>();
            }
            rb.useGravity = true;
            rb.AddForce(Vector3.down * poopForce, ForceMode.Impulse);
        }
    }
}
