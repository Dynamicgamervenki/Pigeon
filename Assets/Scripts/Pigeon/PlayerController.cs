using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CameraManager cameraManager;
    Animator animator;
    CharacterController characterController; // Reference to CharacterController component
    HealthSystem healthSystem;

    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float verticalSpeed = 2f; // Speed for vertical movement when space is pressed
    public GameObject poopPrefab; // Reference to the poop prefab
    public Transform poopSpawnPoint; // Reference to the poop spawn point
    public float poopForce = 10f; // Force to apply to the poop to make it fall

    public float tiltAngle = 30f; // Angle for tilting when Q or E key is pressed
    public float tiltSmoothness = 5f; // Speed of tilt interpolation
    private Quaternion initialRotation; // Store initial rotation for resetting tilt
    private Quaternion targetRotation; // Target rotation for tilting

    public float healthDamage;

    public ParticleSystem blood;

    private void Awake()
    {
        cameraManager = FindObjectOfType<CameraManager>(); // Assuming CameraManager is a script in your scene
        healthSystem = FindObjectOfType<HealthSystem>();
        animator = GetComponent<Animator>(); // Assuming Animator is attached to the same GameObject as this script
        characterController = GetComponent<CharacterController>(); // Get the CharacterController component
        initialRotation = transform.rotation; // Store initial rotation of the bird
        targetRotation = initialRotation; // Initialize target rotation to initial rotation
        blood = this.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        // Get input
        float moveForward = Input.GetAxis("Vertical"); // W/S or Up/Down arrow keys
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow keys

        // Move the pigeon using CharacterController
        Vector3 moveDirection = (transform.forward * moveForward + transform.right * moveHorizontal) * moveSpeed * Time.deltaTime;
        characterController.Move(moveDirection);


        // Calculate the speed based on both forward and horizontal movement for animation blendtree
        float speed = new Vector3(moveForward, 0, moveHorizontal).magnitude;
        animator.SetFloat("Speed", speed);

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
            animator.SetFloat("Speed", 1.0f);
        }

        // Move the pigeon down when S key is pressed
        if (Input.GetKey(KeyCode.S))
        {
            characterController.Move(Vector3.down * verticalSpeed * Time.deltaTime);
            animator.SetFloat("Speed", 1.0f);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CurrentPole")
        {
           healthSystem.slider.value -= healthSystem.decreaseRate * Time.deltaTime;
            // gameObject.SetActive(false);
        }
            
    }
}
