using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    CameraManager cameraManager;
    CameraController cameraController; // Reference to the CameraController script
    Animator animator;
    CharacterController characterController; // Reference to CharacterController component
    HealthSystem healthSystem;

    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float verticalSpeed = 2f; // Speed for vertical movement when space is pressed
    public GameObject poopPrefab; // Reference to the poop prefab
    public Transform poopSpawnPoint; // Reference to the poop spawn point
    public float poopForce = 10f; // Force to apply to the poop to make it fall

    public float tiltAngle = 30f; // Angle for tilting when Q or E key is pressed
    public float tiltSmoothness = 5f; // Speed of tilt interpolation
    private Quaternion initialRotation; // Store initial rotation for resetting tilt
    private Quaternion tiltTargetRotation; // Target rotation for tilting

    public float healthDamage;
    float originalSpeed;

    public ParticleSystem blood;

    private void Awake()
    {
        originalSpeed = moveSpeed;
        cameraManager = FindObjectOfType<CameraManager>(); // Assuming CameraManager is a script in your scene
        cameraController = FindObjectOfType<CameraController>(); // Get the CameraController component
        healthSystem = FindObjectOfType<HealthSystem>();
        animator = GetComponent<Animator>(); // Assuming Animator is attached to the same GameObject as this script
        characterController = GetComponent<CharacterController>(); // Get the CharacterController component
        initialRotation = transform.rotation; // Store initial rotation of the bird
        tiltTargetRotation = initialRotation; // Initialize target rotation to initial rotation
        blood = this.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic("Theme");
    }

    void Update()
    {
        // Get input
        float moveForward = Input.GetAxis("Vertical"); // W/S or Up/Down arrow keys

        // Calculate the camera-relative move direction
        Vector3 moveDirection = cameraController.PlanarRotation * new Vector3(0, 0, moveForward);

        bool isNotMoving = moveForward == 0f;

        // If there is input, set the target rotation based on move direction
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            // Smoothly rotate the player towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        //how to check if player is not pressing any button or moving

        // Move the player
        characterController.Move(moveDirection * moveSpeed);

        // Calculate the speed based on forward movement for animation blendtree
        float speed = Mathf.Abs(moveForward);
        animator.SetFloat("Speed", speed);

        // Set animation parameter for tilting when Q or E key is pressed
        if (Input.GetKey(KeyCode.Q))
        {
            animator.SetFloat("Speed", 1.5f); // Set blend tree speed for tilting left
        }
        else if (Input.GetKey(KeyCode.E))
        {
            animator.SetFloat("Speed", 2.0f); // Set blend tree speed for tilting right
        }
        else
        {
            animator.SetFloat("Speed", speed); // Reset speed parameter to normal
        }

        // Move the pigeon up when space key is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            characterController.Move(Vector3.up * verticalSpeed);
            animator.SetFloat("Speed", 0.5f);
        }

        // Move the pigeon down when S key is pressed
        if (Input.GetKey(KeyCode.S))
        {
            characterController.Move(Vector3.down * verticalSpeed);
        }

        // Make the pigeon poop when ctrl key is pressed
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            Poop();
        }


    }

    void Poop()
    {
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CheckPoint")
        {
            other.gameObject.SetActive(false);
            StartCoroutine(DoubleSpeedForDuration(10.0f));
        }
    }

    private System.Collections.IEnumerator DoubleSpeedForDuration(float duration)
    {
        moveSpeed *= 2; // Double the speed
        yield return new WaitForSeconds(duration);
        moveSpeed = originalSpeed; // Revert back to original speed
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "CurrentPole")
        {
            Debug.Log("health decreasing");
            healthSystem.slider.value -= healthSystem.decreaseRate * Time.deltaTime;
            // gameObject.SetActive(false);
        }
    }
}
