using UnityEngine;

public class BowlerMouseSwing : MonoBehaviour
{
    public float maxSwingAngle = 45f;  // Maximum angle the pendulum can swing
    public float swingSpeed = 5f;      // Speed of swinging

    private Vector3 initialMousePosition;  // Initial position of the mouse when clicked
    private float currentAngle = 0f;      // Current angle of the pendulum
    private float targetAngle = 0f;       // Target angle based on mouse movement
    private bool isDragging = false;      // Flag to check if the mouse is being dragged

    private Rigidbody rb;                // Rigidbody of the pendulum arm
    private Transform pivot;             // The pivot point where the pendulum rotates

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pivot = transform.parent; // Assuming the pivot is the parent of the pendulum arm

        // Optionally freeze the Z rotation to prevent physics from affecting the swinging behavior
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Left mouse button clicked
        {
            isDragging = true;
            initialMousePosition = Input.mousePosition;  // Store the initial mouse position
        }

        if (Input.GetMouseButtonUp(0))  // Left mouse button released
        {
            isDragging = false;
        }

        if (isDragging)
        {
            // Get the mouse movement relative to the initial position
            Vector3 mouseDelta = Input.mousePosition - initialMousePosition;
            // Calculate the target angle based on the mouse's horizontal movement
            targetAngle = Mathf.Clamp(mouseDelta.x / Screen.width * maxSwingAngle, -maxSwingAngle, maxSwingAngle);

            // Smoothly update the current angle towards the target angle
            currentAngle = Mathf.Lerp(currentAngle, targetAngle, Time.deltaTime * swingSpeed);

            // Apply the calculated angle to the pivot's rotation (so pendulum swings around the top)
            pivot.localRotation = Quaternion.Euler(0, 0, currentAngle);
        }
    }
}
