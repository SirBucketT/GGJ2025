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
    
    [SerializeField ]float _timer; //Time left until Game Over is obtained
    [SerializeField] float _lerpSpeed = 0.05f;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject gameUI;
   
    void Start()
    {
        gameOver.SetActive(false);
        timerFront.minValue = 0;
        timerBack.minValue = timerFront.minValue;
        timerFront.maxValue = _timer;
        timerBack.maxValue = _timer + 5;
    }
    
    void Update()
    {
        _timer -= Time.deltaTime;
        timerText.text = _timer.ToString(CultureInfo.CurrentCulture) + ("S");
        
        if (timerFront.value != _timer)
        {
            timerFront.value = _timer;
        }

        if (_timer <= 0)
        {
            _timer = 0;
            Time.timeScale = 0;
            gameOver.SetActive(true);
            gameUI.SetActive(false);
        }
    }

    void LateUpdate()
    {
        if (timerFront.value != timerBack.value)
        {
            timerBack.value = Mathf.Lerp(timerFront.value, timerBack.value, _lerpSpeed);
        }
    }
}
