using UnityEngine;

public class BallThrowControl : MonoBehaviour
{
    public LevelManager levelManager; // Reference to LevelManager

    private Rigidbody rb;
    private bool hasBeenReleased = false; // Whether the ball has been released or not
    private Vector3 startPosition; // Store the ball's initial position
    private float fixedYPosition; // Store the Y position to keep it constant
    public float throwForce = 20f; // Force applied when throwing the ball

    public float maxDragDistance = 5f; // Maximum drag distance (Not used since dragging is disabled)

    // Variable to store the initial position (it could be set from LevelManager or automatically at the start)
    private Vector3 initialPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fixedYPosition = transform.position.y;

        // Set initial position to the current position when the game starts
        initialPosition = transform.position;
        startPosition = initialPosition; // Store the initial position
    }

    void Update()
    {
        // Release the ball when the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && !hasBeenReleased)
        {
            ReleaseBall();
        }
    }

    void ReleaseBall()
    {
        hasBeenReleased = true;
        rb.isKinematic = false; // Allow physics to control the ball now

        // Calculate the throw direction (we want to launch it forward along the x-axis or z-axis)
        Vector3 direction = new Vector3(1, 0, 0); // Launch forward along the X axis (modify this if needed)
        direction.Normalize(); // Ensure the direction is normalized (unit vector)

        // Add force to the ball based on the throwForce
        rb.AddForce(direction * throwForce, ForceMode.Impulse);

        if (levelManager != null)
        {
            levelManager.CheckLevelCompletion(); // Check level completion after releasing the ball
        }
        else
        {
            Debug.LogError("LevelManager reference is missing!");
        }
    }

    // Method to reset the ball position when a new level is loaded
    public void ResetBall()
    {
        Vector3 resetPosition = new Vector3(-14f, -8.5f, 0f); // HARDCODED reset position

        // Reset position to the manually specified position
        transform.position = resetPosition;
        fixedYPosition = resetPosition.y;

        // Reset velocity and angular velocity
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // If you want the ball to be kinematic before the throw starts
        rb.isKinematic = true;

        // Reset the release state
        hasBeenReleased = false;
        startPosition = resetPosition;
    }
}
