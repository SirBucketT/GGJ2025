using System.Collections.Generic;
using UnityEngine;

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
    private float holdTime = 0f;
    private readonly float requiredHoldTime = 2f;
    private OnTriggerEventManager manager;
    private AudioSource audioSource;  // AudioSource to play sound
    public AudioClip soundEffect;  // Sound effect to be played when holding E
    public float scorePoint = 0.0f;

    void Start()
    {
        manager = FindObjectOfType<OnTriggerEventManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKey(KeyCode.E))
        {
            holdTime += Time.deltaTime;

            // Play the sound effect while holding E down
            if (holdTime >= 0f && !audioSource.isPlaying)  // Play once when holding starts
            {
                PlaySoundEffect();
            }

            if (holdTime >= requiredHoldTime)
            {
                TriggerHoldEvent();
                holdTime = 0f;
            }
        }
        else
        {
            holdTime = 0f;
            if (audioSource.isPlaying)  // Stop the sound effect if E is released
            {
                audioSource.Stop();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        isPlayerInTrigger = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isPlayerInTrigger = false;
        holdTime = 0f;
    }

    private void TriggerHoldEvent()
    {
        Destroy(gameObject);  // Destroy the object instead of deactivating it
        scorePoint += 100;

        if (manager != null)
        {
            manager.OnObjectDestroyed();  // Notify the manager that the object is destroyed
        }
    }
    private void PlaySoundEffect()
    {
        if (soundEffect != null && audioSource != null)
        {
            audioSource.PlayOneShot(soundEffect);  // Play the sound effect once when the E key is held
        }
    }
    
}
