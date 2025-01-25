using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player
    public float sprintMultiplier = 2f; // Multiplier for speed when holding Shift (adjust this as needed)

    private Vector2 movement; // Stores the direction of movement
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip drivingSound; // Driving sound effect
    public AudioClip idleSound; // Idle sound effect
    private bool isMoving = false; // Track if the player is moving or idle

    void Start()
    {
        // Get the Rigidbody2D component attached to the player
        rb = GetComponent<Rigidbody2D>();

        // Get the AudioSource component attached to the player
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Get input from the WASD keys
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Check if the player is holding the Shift key
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            // Increase speed by multiplying the moveSpeed
            moveSpeed = 5f * sprintMultiplier; // Default speed (5f) multiplied by sprintMultiplier
        }
        else
        {
            moveSpeed = 5f; // Default speed when Shift is not held
        }

        // Create the movement vector based on input
        movement = new Vector2(moveX, moveY); // Using Vector2 for movement in 2D

        // Check if the player is moving
        if (movement.magnitude > 0.1f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        // Rotate the player to face the movement direction if moving
        if (isMoving)
        {
            // Calculate angle to rotate towards movement direction (in 2D)
            float angle = Mathf.Atan2(-movement.x, movement.y) * Mathf.Rad2Deg;
            rb.rotation = angle; // Set the 2D rotation directly

            // Play the driving sound if it's not already playing and the sound is assigned
            if (drivingSound != null && (!audioSource.isPlaying || audioSource.clip != drivingSound))
            {
                audioSource.Stop(); // Stop the idle sound (if playing)
                audioSource.PlayOneShot(drivingSound); // Play the driving sound
            }
        }
        else
        {
            // Play the idle sound if it's not already playing and the sound is assigned
            if (idleSound != null && (!audioSource.isPlaying || audioSource.clip != idleSound))
            {
                audioSource.Stop(); // Stop the driving sound (if playing)
                audioSource.PlayOneShot(idleSound); // Play the idle sound
            }
        }
    }

    void FixedUpdate()
    {
        // Apply movement to the Rigidbody2D
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
