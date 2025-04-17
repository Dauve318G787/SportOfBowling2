using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject level1Prefab; // Prefab for level 1
    public GameObject level2Prefab; // Prefab for level 2
    public GameObject level3Prefab; // Prefab for level 3

    private GameObject currentLevel;
    private int currentLevelIndex = 1; // Current level index (1 = level 1, etc.)
    private BowlingPin[] allPins;  // Array to store all pins in the current level

    void Start()
    {
        LoadLevel(currentLevelIndex);
        InvokeRepeating("CheckLevelCompletion", 1f, 1f); // Check level completion every second (starting after 1 second)
    }

    void Update()
    {
        // Optional: You could also check for a manual completion here, but the InvokeRepeating will do it every second
    }

    // Load the specific level (level 1, 2, etc.)
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
            default:
                Debug.LogError("Invalid level number");
                break;
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

        if (currentLevelIndex > 3)
        {
            Debug.Log("Game Complete!");
            // Optionally, show a victory screen or restart the game
        }
        else
        {
            LoadLevel(currentLevelIndex); // Load the next level
        }
    }
}
