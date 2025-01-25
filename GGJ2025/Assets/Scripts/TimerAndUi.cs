using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TimerAndUi : MonoBehaviour
{
    [Header("Slider Elements")]
    [SerializeField] Slider timerFront;
    [SerializeField] Slider timerBack;
    
    [Header("UI Text Elements")]
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text scoreValueText;
    
    [SerializeField] float timer = 10f;  
    [SerializeField] float lerpSpeed = 0.05f;
    
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
        timerBack.value = timer + 2;
        
        timerBack.value = Mathf.Lerp(timerBack.value, timerFront.value, lerpSpeed * Time.deltaTime);
        
        timerText.text = timer.ToString("F2", CultureInfo.CurrentCulture) + "S";
    }
}
