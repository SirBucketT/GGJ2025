using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class TimerAndUi : MonoBehaviour
{
    [Header("Slider Elements")]
    [SerializeField] Slider timerFront;
    [SerializeField] Slider timerBack;
    
    [Header("UI Text Elements")]
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text gameOverValueText;
    
    [SerializeField] float _timer = 10f;  
    [SerializeField] float _lerpSpeed = 0.05f;
    
    [Header("Game Over Elements")]
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject gameUI;

    private void Start()
    {
        timerFront.minValue = 0f;
        timerFront.maxValue = _timer;
        timerBack.minValue = 0f;
        timerBack.maxValue = _timer;

        gameOver.SetActive(false);
    }
    
    private void Update()
    {
        _timer -= Time.deltaTime;
        
        if (_timer <= 0f)
        {
            _timer = 0f;
            
            Time.timeScale = 0f;
            
            gameOver.SetActive(true);
            gameUI.SetActive(false);
        }
        
        timerFront.value = _timer;
        timerBack.value = _timer + 2;
        
        timerBack.value = Mathf.Lerp(timerBack.value, timerFront.value, _lerpSpeed * Time.deltaTime);
        
        timerText.text = _timer.ToString("F2", CultureInfo.CurrentCulture) + "S";
    }
}
