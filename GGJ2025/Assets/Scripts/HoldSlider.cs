using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HoldSlider : MonoBehaviour
{
    [SerializeField] OnTriggerEvent onTriggerEvent;
    public Slider holdSlider;
    
    void Start()
    {
        if (holdSlider != null)
        {
            holdSlider.minValue = 0f;
            holdSlider.maxValue = onTriggerEvent.requiredHoldTime;
            holdSlider.value = 0f;
            holdSlider.gameObject.SetActive(false);
        }
    }
    
    void Update()
    {
        // Update slider fill if assigned
        if (holdSlider != null)
        {
            holdSlider.value = onTriggerEvent.holdTime;
        }
    }

    public void Trigger()
    {
        holdSlider.gameObject.SetActive(true);
    }

    public void UnTrigger()
    {
        holdSlider.gameObject.SetActive(false);
    }
}
