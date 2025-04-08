using UnityEngine;

public class BowlerSwing : MonoBehaviour
{
    public GameObject projectile;  // The projectile GameObject to be released
    public Transform releasePoint; // The point from which the projectile will be released
    public float releaseAngle = 45f; // Angle at which to release the projectile

    private bool released = false; // To prevent multiple releases

    void Update()
    {
        // Get the current angle of the pendulum arm
        float angle = transform.localEulerAngles.z; // Z-axis rotation in 2D

        // Normalize angle to be between -180 and 180 degrees for ease
        if (angle > 180) angle -= 360;

        // Check if the pendulum has passed the release angle and hasn't already released the projectile
        if (Mathf.Abs(angle) >= releaseAngle && !released)
        {
            ReleaseProjectile();
        }
    }

    void ReleaseProjectile()
    {
        released = true; // Ensure the projectile is only released once

        // Instantiate the projectile at the release point
        GameObject proj = Instantiate(projectile, releasePoint.position, releasePoint.rotation);

        // Get the Rigidbody of the projectile
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; // Allow the projectile to move under physics
            rb.AddForce(Vector3.right * 10f, ForceMode.Impulse); // Adjust force as needed
        }

        // Optionally destroy the projectile after some time to clean up
        Destroy(proj, 5f);
    }
}
