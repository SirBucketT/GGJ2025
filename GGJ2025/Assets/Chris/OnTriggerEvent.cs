using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnTriggerEventManager : MonoBehaviour
{
    public List<GameObject> objects;
    private int currentIndex = 0;
    private int activeCount = 0;
    
    void Awake() // Use Awake instead of Start
    {
        Debug.LogError("SCENE LOADED - CHECKING OBJECTS");

        // Force log number of objects
        Debug.LogError($"Total Objects in List: {objects.Count}");
    }

    void Start()
    {
        // Find all objects with OnTriggerEvent script if manual assignment fails
        if (objects == null || objects.Count == 0)
        {
            objects = new List<GameObject>();
            OnTriggerEvent[] triggerEvents = FindObjectsOfType<OnTriggerEvent>();
            foreach (OnTriggerEvent evt in triggerEvents)
            {
                objects.Add(evt.gameObject);
            }
        }

        // Forcibly activate initialization
        if (objects.Count > 0)
        {
            ActivateNextSet();
        }
    }

    public void ActivateNextSet()
    {
        Debug.LogError("ACTIVATE NEXT SET CALLED");

        // Deactivate current 5 objects
        for (int i = 0; i < 5; i++)
        {
            int index = (currentIndex + i) % objects.Count;
            objects[index].SetActive(false);
        }

        // Move to next set
        currentIndex = (currentIndex + 5) % objects.Count;

        // Activate next 5 objects
        activeCount = 5;
        for (int i = 0; i < 5; i++)
        {
            int index = (currentIndex + i) % objects.Count;
            objects[index].SetActive(true);
        }
    }

    public void OnObjectDestroyed()
    {
        Debug.LogError("OBJECT DESTROYED");

        activeCount--;
        if (activeCount <= 0)
        {
            ActivateNextSet();
        }
    }
}

public class OnTriggerEvent : MonoBehaviour
{
    private bool isPlayerInTrigger = false;
    public float holdTime = 0f;
    public readonly float requiredHoldTime = 2f;

    private OnTriggerEventManager manager;
    public HoldSlider holdSlider;
    private AudioSource audioSource;
    public AudioClip soundEffect;
    public float scorePoint;

    private void Start()
    {
        manager = FindObjectOfType<OnTriggerEventManager>();
        audioSource = GetComponent<AudioSource>();
        scorePoint = 0.0f;
    }

    private void Update()
    {
        // If player is inside trigger and holding E, accumulate holdTime
        if (isPlayerInTrigger && Input.GetKey(KeyCode.E))
        {
            holdTime += Time.deltaTime;

            // Play sound once at the start of holding
            if (!audioSource.isPlaying && holdTime > 0f)
            {
                PlaySoundEffect();
            }

            // Check if we've held long enough
            if (holdTime >= requiredHoldTime)
            {
                TriggerHoldEvent();
                ResetHoldTime();
            }
        }
        else
        {
            // Not pressing E or left the trigger => reset hold if needed
            if (holdTime > 0f)
            {
                ResetHoldTime();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        isPlayerInTrigger = true;

        
        // Show the slider upon entering the trigger
        holdSlider.Trigger();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isPlayerInTrigger = false;

        // Hide slider and reset hold time when exiting
        ResetHoldTime();
        holdSlider.UnTrigger();
    }

    private void TriggerHoldEvent()
    {
        if (manager != null)
        {
            manager.OnObjectDestroyed();
        }

        // Add score using a global ScoreManager (two-parameter version)
        ScoreManager.Instance.AddScore(100, 100);

        Debug.Log("Block destroyed, +100 points!");
        Destroy(gameObject);  
    }

    private void PlaySoundEffect()
    {
        if (soundEffect != null && audioSource != null)
        {
            audioSource.PlayOneShot(soundEffect);
        }
    }

    private void ResetHoldTime()
    {
        holdTime = 0f;
        
        
        if (holdSlider != null)
        {
            holdSlider.holdSlider.value = 0f;
        }
        
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}