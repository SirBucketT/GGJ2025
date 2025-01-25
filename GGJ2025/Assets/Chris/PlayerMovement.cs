using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player

    private Vector2 movement; // Stores the direction of movement
    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    void Start()
    {
        // Get the Rigidbody2D component attached to the player
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from the WASD keys
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Create the movement vector based on input
        movement = new Vector2(moveX, moveY); // Using Vector2 for movement in 2D

        // Rotate the player to face the movement direction if moving
        if (movement.magnitude > 0.1f)
        {
            // Calculate angle to rotate towards movement direction (in 2D)
            float angle = Mathf.Atan2(-movement.x, movement.y) * Mathf.Rad2Deg;
            rb.rotation = angle; // Set the 2D rotation directly
        }
    }

    void FixedUpdate()
    {
        // Apply movement to the Rigidbody2D
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}