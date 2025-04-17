using UnityEngine;

public class BallThrowControl : MonoBehaviour
{
    private Rigidbody rb;
    private bool isDragging = false;
    private bool hasBeenReleased = false; // Ensure the player can only throw once
    private Vector3 startPosition;
    private float fixedYPosition;
    public float throwForce = 5f;

    // New variable to limit the drag distance
    public float maxDragDistance = 0.075f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fixedYPosition = transform.position.y;
    }

    void Update()
    {
        // Only allow drag if mouse is down, and ball hasn’t been released yet
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

        // Calculate the distance between the start position and the current mouse position
        Vector3 dragVector = mouseWorld - startPosition;

        // Limit the drag distance to the max drag distance
        if (dragVector.magnitude > maxDragDistance)
        {
            dragVector = dragVector.normalized * maxDragDistance;
        }

        // Prevent dragging into negative X direction
        float clampedX = Mathf.Max(dragVector.x, 0f);

        // Allow only X+ and Z movement, and lock Y
        transform.position = startPosition + new Vector3(clampedX, 0f, dragVector.z);
    }

    void ReleaseBall()
    {
        isDragging = false;
        hasBeenReleased = true; // Prevent dragging again
        rb.isKinematic = false;

        // Calculate the direction and speed to apply force
        Vector3 direction = new Vector3(
            transform.position.x - startPosition.x,
            0f,
            transform.position.z - startPosition.z
        );

        // Clamp the speed to ensure it's within a reasonable range
        float clampedSpeed = Mathf.Clamp(direction.magnitude, 0, throwForce);
        rb.AddForce(direction.normalized * clampedSpeed, ForceMode.Impulse);
    }
}
