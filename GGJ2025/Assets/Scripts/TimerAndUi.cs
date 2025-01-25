using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Globalization;

public class TimerAndUi : MonoBehaviour
{
    [SerializeField] ScoreManager _scoreManager;

    [Header("Slider Elements")]
    [SerializeField] Slider timerFront;
    [SerializeField] Slider timerBack;

    [Header("UI Text Elements")]
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text timerTextBack;
    [SerializeField] TMP_Text scoreValueText;
    [SerializeField] TMP_Text timerValueTextBack;

    [SerializeField] float timer = 10f;  
    [SerializeField] float lerpSpeed = 0.05f;
    [SerializeField] float delayTime = 2f;

    [Header("Game Over Elements")]
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject gameUI;

    private void Start()
    {
        timerFront.minValue = 0f;
        timerFront.maxValue = timer;
        timerBack.minValue = 0f;
        timerBack.maxValue = timer;

        gameOver.SetActive(false);

        if (_scoreManager != null)
        {
            scoreValueText.text      = _scoreManager.score.ToString("F2", CultureInfo.CurrentCulture);
            timerValueTextBack.text = _scoreManager.score.ToString("F2", CultureInfo.CurrentCulture);
        }
        else
        {
            Debug.LogWarning("ScoreManager is not assigned!");
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = 0f;
            Time.timeScale = 0f;
            gameOver.SetActive(true);
            gameUI.SetActive(false);
        }

        timerFront.value = timer;
        timerBack.value  = Mathf.Lerp(timerBack.value, timerFront.value, lerpSpeed * Time.deltaTime);

        timerText.text     = timer.ToString("F2", CultureInfo.CurrentCulture) + "S";
        timerTextBack.text = timerText.text;

        if (_scoreManager != null)
        {
            scoreValueText.text = Mathf.FloorToInt(_scoreManager.score).ToString();
            timerValueTextBack.text = Mathf.FloorToInt(_scoreManager.score).ToString();
        }
    }
}
