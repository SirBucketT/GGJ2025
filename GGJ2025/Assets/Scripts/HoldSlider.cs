using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HoldSlider : MonoBehaviour
{
    public OnTriggerEvent onTriggerEvent;
    public Slider holdSlider;
    
    void Start()
    {
        holdSlider.minValue = 0f;
        holdSlider.maxValue = onTriggerEvent.requiredHoldTime;
        holdSlider.value = 0f;
        //holdSlider.gameObject.SetActive(true);
        holdSlider.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        OnTriggerEvent.OnShowProgress += OnProgressUpdated;
    }

    private void OnDisable()
    {
        OnTriggerEvent.OnShowProgress -= OnProgressUpdated;
    }

    private void OnDestroy()
    {
        Debug.LogError("Hold Slider has been Destroyed");
    }

    private void OnProgressUpdated(float progress)
    {
        // Check if holdSlider is not null before accessing its value
        if (holdSlider != null)
        {
            holdSlider.value = progress;

            // Check if onTriggerEvent is not null before accessing requiredHoldTime
            if (onTriggerEvent != null && holdSlider.value >= onTriggerEvent.requiredHoldTime)
            {
                UnTrigger();
            }
        }
        else
        {
            Debug.LogWarning("HoldSlider is null; cannot update progress.");
        }
    }

    // void Update()
    // {
    //     // Update slider fill if assigned
    //     if (holdSlider != null)
    //     {
    //         holdSlider.value = onTriggerEvent.holdTime;
    //     }
    // }

    public void Trigger()
    {
        holdSlider.gameObject.SetActive(true);
    }

    public void UnTrigger()
    {
        holdSlider.gameObject.SetActive(false);
    }
}
