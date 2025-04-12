using UnityEngine;

public class BowlingPin : MonoBehaviour
{
    private bool hasScored = false;

    void Update()
    {
        float tilt = Vector3.Angle(transform.up, Vector3.up);

        if (!hasScored && tilt > 30f) // 30 degrees tilt = fallen
        {
            hasScored = true;
            ScoreCounter.instance.AddScore(1);
        }
    }
}
