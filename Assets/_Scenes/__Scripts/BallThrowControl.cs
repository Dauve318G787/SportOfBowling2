using UnityEngine;

public class BallThrowControl : MonoBehaviour
{
    private Rigidbody rb;
    private bool isDragging = false;   // Flag to check if we're dragging the ball
    private Vector3 startPosition;     // Starting position of the ball
    private Vector3 mouseOffset;       // Offset to make the drag feel smoother

    public float throwForce = 10f; // Force applied when the ball is released

    void Start()
    {
        // Get the Rigidbody component from the ball
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Detect mouse click to start dragging the ball
        if (Input.GetMouseButtonDown(0)) // 0 = Left Mouse Button
        {
            StartDrag();
        }

        // Detect mouse movement to drag the ball
        if (isDragging)
        {
            DragBall();
        }

        // Detect mouse release to throw the ball
        if (Input.GetMouseButtonUp(0)) // 0 = Left Mouse Button
        {
            ReleaseBall();
        }
    }

    // Start dragging the ball when the mouse is clicked
    void StartDrag()
    {
        isDragging = true;
        startPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)); // Convert mouse position to world space
        mouseOffset = startPosition - transform.position;  // Set an offset so the ball doesn't jump to the mouse position
    }

    // While dragging, update the ball's position based on the mouse position
    void DragBall()
    {
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        transform.position = currentMousePosition - mouseOffset; // Update the ball's position
    }

    // Release the ball and apply a force in the direction the ball is facing
    void ReleaseBall()
    {
        isDragging = false;
        Vector3 direction = transform.position - startPosition; // The direction the ball is being thrown
        rb.isKinematic = false; // Enable physics again (in case it was set to kinematic for dragging)
        rb.AddForce(direction * throwForce, ForceMode.Impulse); // Apply the throw force
    }
}
