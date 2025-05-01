using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Level prefabs
    public GameObject level1Prefab;
    public GameObject level2Prefab;
    public GameObject level3Prefab;
    public GameObject level4Prefab;

    private GameObject currentLevel;
    private int currentLevelIndex = 1; // Current level index (1 = level 1, etc.)
    private BowlingPin[] allPins;  // Array to store all pins in the current level

    public BallThrowControl ballThrowControl; // Reference to the BallThrowControl

    void Start()
    {
        LoadLevel(currentLevelIndex);
        InvokeRepeating("CheckLevelCompletion", 1f, 1f); // Check level completion every second (starting after 1 second)
    }

    // Load the specific level
    public void LoadLevel(int level)
    {
        // Destroy the current level if it exists
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }

        // Instantiate the new level based on the level number
        switch (level)
        {
            case 1:
                currentLevel = Instantiate(level1Prefab);
                break;
            case 2:
                currentLevel = Instantiate(level2Prefab);
                break;
            case 3:
                currentLevel = Instantiate(level3Prefab);
                break;
            case 4:
                currentLevel = Instantiate(level4Prefab);
                break;
            default:
                Debug.LogError("Invalid level number");
                break;
        }

        // If level 1 is loaded, increase the throw force by 10
        if (level == 1 && ballThrowControl != null)
        {
            ballThrowControl.throwForce += 10f; // Increase throw force
            Debug.Log("Throw force increased to: " + ballThrowControl.throwForce);
        }

        // Call ResetBall() after the level has loaded
        if (ballThrowControl != null)
        {
            ballThrowControl.ResetBall();
            Debug.Log("Resetting Ball Position...");
        }

        // Get all BowlingPin objects in the current level
        allPins = FindObjectsOfType<BowlingPin>();
    }

    // Check if all pins have fallen
    private bool AreAllPinsDown()
    {
        foreach (BowlingPin pin in allPins)
        {
            if (!pin.hasScored) // If any pin has not scored, it hasn't fallen
            {
                return false; // Return false if any pin hasn't fallen
            }
        }
        return true; // All pins have fallen
    }

    // Call this when the player completes a level
    public void CheckLevelCompletion()
    {
        if (AreAllPinsDown())
        {
            CompleteLevel();
        }
        else
        {
            // Debug message for checking if all pins are down
            Debug.Log("Not all pins are knocked down yet!");
        }
    }

    public void CompleteLevel()
    {
        currentLevelIndex++;

        // If level index exceeds 4, reset to 1 to loop the levels
        if (currentLevelIndex > 4)
        {
            currentLevelIndex = 1;  // Loop back to level 1
            Debug.Log("Looping back to Level 1!");
        }

        LoadLevel(currentLevelIndex); // Load the next level or loop back to 1
    }
}
