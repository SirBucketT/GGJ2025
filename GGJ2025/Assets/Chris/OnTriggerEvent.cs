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

    public void OnObjectDisabled()
    {
        Debug.LogError("OBJECT DISABLED");
        
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

    void Start()
    {
        manager = FindObjectOfType<OnTriggerEventManager>();
    }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKey(KeyCode.E))
        {
            holdTime += Time.deltaTime;

            if (holdTime >= requiredHoldTime)
            {
                TriggerHoldEvent();
                holdTime = 0f;
            }
        }
        else
        {
            holdTime = 0f;
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
        Debug.Log("E key held for 2 seconds! Event triggered.");
        gameObject.SetActive(false);

        if (manager != null)
        {
            manager.OnObjectDisabled();
        }
    }
}   