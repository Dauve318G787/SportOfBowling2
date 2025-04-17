using UnityEngine;

public class BowlingPin : MonoBehaviour
{
    public bool hasScored = false;

    void Update()
    {
        // Calculate the tilt angle between the pin's up direction and the world up direction
        float tilt = Vector3.Angle(transform.up, Vector3.up);

        // // Log the tilt value to the console for debugging purposes
        // Debug.Log("Pin Tilt: " + tilt);

        // Check if the pin has tilted past 30 degrees
        if (!hasScored && tilt > 30f) // 30 degrees tilt = fallen
        {
            hasScored = true;
            ScoreCounter.instance.AddScore(1); // Add score when the pin falls
        }
    }
}
