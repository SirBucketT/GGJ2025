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
    private bool isDrivingSoundPlaying = false; // Track if driving sound is playing
    private float timer = 0f; // Timer to track time for audio pause

    void Start()
    {
        // Get the Rigidbody2D component attached to the player
        rb = GetComponent<Rigidbody2D>();

        // Get the AudioSource component attached to the player
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Increment the timer by the time passed each frame
        timer += Time.deltaTime;

        // Pause all audio after 60 seconds
        if (timer >= 60f)
        {
            StopAudio();
        }

        // Check if the game is paused (e.g., Esc key is pressed)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle pause state
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0; // Pause the game
                StopAudio(); // Stop the audio when paused
            }
            else
            {
                Time.timeScale = 1; // Unpause the game
                PlayAudio(); // Resume audio if the player is moving
            }
            return; // Early return to prevent further updates during pause
        }

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
            if (drivingSound != null && !isDrivingSoundPlaying)
            {
                audioSource.Stop(); // Stop the idle sound (if playing)
                audioSource.clip = drivingSound; // Set the driving sound as the clip
                audioSource.loop = true; // Make sure the driving sound loops
                audioSource.Play(); // Start playing the driving sound
                isDrivingSoundPlaying = true; // Mark that the driving sound is playing
            }
        }
        else
        {
            // Stop the driving sound if the player is idle
            if (isDrivingSoundPlaying)
            {
                audioSource.Stop(); // Stop the driving sound
                isDrivingSoundPlaying = false;
            }

            // Play the idle sound if it's not already playing and the sound is assigned
            if (idleSound != null && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(idleSound); // Play the idle sound
            }
        }
    }

    void FixedUpdate()
    {
        // Apply movement to the Rigidbody2D
        if (Time.timeScale == 1) // Only move the player if the game is unpaused
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    // Function to stop the audio
    private void StopAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause(); // Pause the audio if it's playing
        }
    }

    // Function to resume the audio when unpaused
    private void PlayAudio()
    {
        if (!audioSource.isPlaying && isDrivingSoundPlaying)
        {
            audioSource.Play(); // Resume playing if it's not already playing
        }
    }
}
