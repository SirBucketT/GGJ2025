using UnityEngine;
using System.Collections;

public class OnTriggerEvent : MonoBehaviour
{
    // “other” refers to the collider on the GameObject inside this trigger
    void OnTriggerEnter (Collider other)
    {
        Debug.Log ("A collider has entered the OnTriggerEvent trigger");
    }

    void OnTriggerStay (Collider other)
    {
        Debug.Log ("A collider is inside the OnTriggerEvent trigger");
    }
    
    void OnTriggerExit (Collider other)
    {
        Debug.Log ("A collider has exited the OnTriggerEvent trigger");
    }
}
