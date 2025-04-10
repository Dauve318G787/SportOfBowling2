using UnityEngine;

public class BallThrowControl : MonoBehaviour
{
    private Rigidbody rb;
    private bool isDragging = false;
    private bool hasBeenReleased = false; // Ensure the player can only throw once
    private Vector3 startPosition;
    private float fixedYPosition;
    public float throwForce = 5f;

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

        // Prevent dragging into negative X direction
        float clampedX = Mathf.Max(mouseWorld.x, startPosition.x);

        // Allow only X+ and Z movement, and lock Y
        transform.position = new Vector3(clampedX, fixedYPosition, mouseWorld.z);
    }

    void ReleaseBall()
    {
        isDragging = false;
        hasBeenReleased = true; // Prevent dragging again
        rb.isKinematic = false;

        Vector3 direction = new Vector3(
            transform.position.x - startPosition.x,
            0f,
            transform.position.z - startPosition.z
        );

        float clampedSpeed = Mathf.Clamp(direction.magnitude, 0, throwForce);
        rb.AddForce(direction.normalized * clampedSpeed, ForceMode.Impulse);
    }
}
