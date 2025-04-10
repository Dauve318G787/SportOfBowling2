using UnityEngine;

public class BallThrowControl : MonoBehaviour
{
    private Rigidbody rb;
    private bool isDragging = false;
    private Vector3 startPosition;
    private float fixedYPosition; // Locks Y position
    public float throwForce = 5f; // Modify this for a harder throw

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fixedYPosition = transform.position.y;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrag();
        }

        if (isDragging)
        {
            DragBall();
        }

        if (Input.GetMouseButtonUp(0))
        {
            ReleaseBall();
        }
    }

    void StartDrag()
    {
        isDragging = true;
        startPosition = transform.position;
        rb.isKinematic = true; // Disable physics while dragging
    }

    void DragBall()
    {
        // Get mouse position in world space, using depth of 10 units from camera
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));

        // Only allow X and Z movement, keep Y fixed
        transform.position = new Vector3(mouseWorld.x, fixedYPosition, mouseWorld.z);
    }

    void ReleaseBall()
    {
        isDragging = false;
        rb.isKinematic = false;

        // Calculate throw direction only in X/Z plane
        Vector3 direction = new Vector3(
            transform.position.x - startPosition.x,
            0f, // No vertical force
            transform.position.z - startPosition.z
        );

        float clampedSpeed = Mathf.Clamp(direction.magnitude, 0, throwForce);
        rb.AddForce(direction.normalized * clampedSpeed, ForceMode.Impulse);
    }
}
