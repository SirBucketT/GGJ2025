using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; } // Singleton instance

    public int score = 0; // The player's current score
    [SerializeField] TMP_Text scoreGainText; //score gain effect
    [SerializeField] TMP_Text scoreGainBackText;

    void Start()
    {
        scoreGainText.gameObject.SetActive(false);
        scoreGainBackText.gameObject.SetActive(false);
    }

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
    public void AddScore(int points, int ScoreGain)
    {
        score += points;
        StartCoroutine(ShowScoreGain(ScoreGain));
        Debug.Log($"Score: {score}"); // Log the updated score
        UpdateScoreUI(); // Update any UI, if needed
    }
    private System.Collections.IEnumerator ShowScoreGain(int ScoreGain)
    {
        scoreGainText.text = $"+{ScoreGain}";  
        scoreGainBackText.text = $"+{ScoreGain}";
        scoreGainText.gameObject.SetActive(true);
        scoreGainBackText.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(1f);  

        scoreGainText.gameObject.SetActive(false);
        scoreGainBackText.gameObject.SetActive(false);
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