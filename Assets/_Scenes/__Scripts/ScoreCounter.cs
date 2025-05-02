// MODULE PURPOSE: To track the high score and current score.

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public int score = 0; // default value for the regular score
    public int highScore = 0; // default value for high score
    public TMP_Text scoreText; // GameObject for score
    public TMP_Text highScoreText; // GameObject for high score

    public static ScoreCounter instance; // Create an instance for the score counter

    // Start out with high score at 0, fetch PlayerPrefs data if there is any
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreText();
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Wrapper function to add score
    public void AddScore(int points)
    {
        score += points;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        UpdateScoreText();
    }

    // Wrapper function to update score
    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
        
        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore;
    }
}
