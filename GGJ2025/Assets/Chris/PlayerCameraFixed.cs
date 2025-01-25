using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform player; // Reference to the player GameObject
    public Vector3 offset = new Vector3(0, 0, 0); // Fixed position offset for the camera

    void LateUpdate()
    {
        if (player != null)
        {
            // Update the camera's position to follow the player while maintaining a fixed offset
            transform.position = player.position + offset;

            // Keep the camera's rotation fixed (no tilt or rotation)
            transform.rotation = Quaternion.Euler(0, 0, 0); // Set the rotation to fixed top-down
        }
    }
}