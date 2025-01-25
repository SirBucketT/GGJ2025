using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform player; // Reference to the player GameObject
    public Vector3 offset = new Vector3(0, 10, 0); // Fixed position offset for the camera

    void LateUpdate()
    {
        // Keep the camera fixed above the player at the specified offset
        if (player != null)
        {
            transform.position = player.position + offset;
            transform.rotation = Quaternion.Euler(90, 0, 0); // Ensure the camera is always looking down
        }
    }
}