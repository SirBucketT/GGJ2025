using UnityEngine;
using System.Collections;

public class OnTriggerEvent : MonoBehaviour
{
    private bool isPlayerInTrigger = false;
    private float holdTime = 0f; // Tracks how long E is held
    private readonly float requiredHoldTime = 5f; // Time required to trigger the event

    void Update()
    {
        // Check if the player is in the trigger area and holding the E key
        if (isPlayerInTrigger && Input.GetKey(KeyCode.E))
        {
            holdTime += Time.deltaTime; // Increment the hold time

            if (holdTime >= requiredHoldTime)
            {
                TriggerHoldEvent();
                holdTime = 0f; // Reset hold time after triggering the event
            }
        }
        else
        {
            holdTime = 0f; // Reset hold time if E is not being held
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("A collider has entered the OnTriggerEvent trigger");
    }

    void OnTriggerStay(Collider other)
    {
        // Debug.Log("A collider is inside the OnTriggerEvent trigger");
        isPlayerInTrigger = true; // Set the flag to true when in the trigger
    }

    void OnTriggerExit(Collider other)
    {
        // Debug.Log("A collider has exited the OnTriggerEvent trigger");
        isPlayerInTrigger = false; // Reset the flag when leaving the trigger
        holdTime = 0f; // Reset the hold time
    }

    private void TriggerHoldEvent()
    {
        Debug.Log("E key held for 5 seconds! Event triggered.");
        // Add your event logic here
    }
}