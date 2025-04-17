using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject level1Prefab; // Prefab for level 1
    public GameObject level2Prefab; // Prefab for level 2
    public GameObject level3Prefab; // Prefab for level 3

    private GameObject currentLevel;  // Track the current instantiated level

    private int currentLevelIndex = 1; // Current level index (1 = level 1, etc.)

    void Start()
    {
        // Initialize the game by loading the first level
        LoadLevel(currentLevelIndex);
    }

    // Load a specific level (level 1, 2, etc.)
    public void LoadLevel(int level)
    {
        // Destroy the current level if it exists
        if (currentLevel != null)
        {
            Destroy(currentLevel); // Remove the current level prefab
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

        // Optionally, you could add a delay or transition effect before loading the next level
    }

    // Call this when the player completes a level
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
