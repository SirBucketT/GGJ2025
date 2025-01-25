using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; } // Singleton instance

    private int score = 0; // The player's current score

    void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist between scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    // Adds points to the score
    public void AddScore(int points)
    {
        score += points;
        Debug.Log($"Score: {score}"); // Log the updated score
        UpdateScoreUI(); // Update any UI, if needed
    }

    // Returns the current score
    public int GetScore()
    {
        return score;
    }

    // Resets the score (e.g., at the start of a new game)
    public void ResetScore()
    {
        score = 0;
        Debug.Log("Score reset to 0.");
        UpdateScoreUI();
    }

    // Placeholder for updating score UI (implement as needed)
    private void UpdateScoreUI()
    {
        // Example: Use UnityEngine.UI to update a score text
        // scoreText.text = $"Score: {score}";
    }
}