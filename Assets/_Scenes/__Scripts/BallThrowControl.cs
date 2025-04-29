using UnityEngine;

public class BallThrowControl : MonoBehaviour
{
    public LevelManager levelManager; // Reference to LevelManager

    private Rigidbody rb;
    private bool isDragging = false;
    private bool hasBeenReleased = false;
    private Vector3 startPosition;
    private float fixedYPosition;
    public float throwForce = 5f;

    public float maxDragDistance = 5f; // Maximum drag distance

    // Variable to store the initial position (it could be set from LevelManager or automatically at the start)
    private Vector3 initialPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fixedYPosition = transform.position.y;

        // Set initial position to the current position when the game starts
        initialPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasBeenReleased)
        {
            StartDrag();
        }

        if (isDragging)
        {
            DragBall();
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            ReleaseBall();
        }
    }

    void StartDrag()
    {
        isDragging = true;
        startPosition = transform.position;
        rb.isKinematic = true;
    }

    void DragBall()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));

        // Calculate the direction vector from the start position to the mouse position
        Vector3 dragDirection = mouseWorld - startPosition;

        // Limit the dragging distance to the maxDragDistance
        if (dragDirection.magnitude > maxDragDistance)
        {
            dragDirection = dragDirection.normalized * maxDragDistance; // Clamp to max distance
        }

        // Set the ball position based on the clamped drag direction
        transform.position = new Vector3(startPosition.x + dragDirection.x, fixedYPosition, startPosition.z + dragDirection.z);
    }

    void ReleaseBall()
    {
        isDragging = false;
        hasBeenReleased = true;
        rb.isKinematic = false;

        // Calculate the throw direction
        Vector3 direction = new Vector3(transform.position.x - startPosition.x, 0f, transform.position.z - startPosition.z);
        float clampedSpeed = Mathf.Clamp(direction.magnitude, 0, throwForce);

        // Add force to the ball based on the drag distance
        rb.AddForce(direction.normalized * clampedSpeed, ForceMode.Impulse);

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
    Vector3 resetPosition = new Vector3(-14f, -9f, 0f); // HARDCODED reset position

    // Reset position to the manually specified position
    transform.position = resetPosition;
    fixedYPosition = resetPosition.y;

    // Reset velocity and angular velocity
    rb.velocity = Vector3.zero;
    rb.angularVelocity = Vector3.zero;

    // If you want the ball to be kinematic before drag starts
    rb.isKinematic = true;

    // Reset the drag and release state
    startPosition = resetPosition;
    isDragging = false;
    hasBeenReleased = false;
    }


}
