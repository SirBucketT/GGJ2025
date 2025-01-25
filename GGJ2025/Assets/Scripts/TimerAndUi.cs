using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TimerAndUi : MonoBehaviour
{
    [Header("Slider Elements")]
    [SerializeField] Slider timerFront;
    [SerializeField] Slider timerBack;
    
    [Header("UI Text Elements")]
    [SerializeField] TMP_Text timerText;
    
    [SerializeField ]float _timer; //Time left until Game Over is obtained
    [SerializeField] float _lerpSpeed = 0.05f;
    [SerializeField] GameObject gameOver;
   
    void Start()
    {
        gameOver.SetActive(false);
    }
    
    void Update()
    {
        if (timerFront.value != _timer)
        {
            timerFront.value = _timer;
        }

        if (timerFront.value != timerBack.value)
        {
            timerBack.value = Mathf.Lerp(timerBack.value, timerFront.value, _lerpSpeed);
        }

        if (_timer <= 0)
        {
            _timer = 0;
            Time.timeScale = 0;
            gameOver.SetActive(true);
        }
    }
}
