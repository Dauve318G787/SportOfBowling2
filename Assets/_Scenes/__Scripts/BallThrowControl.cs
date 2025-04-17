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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fixedYPosition = transform.position.y;
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
}
