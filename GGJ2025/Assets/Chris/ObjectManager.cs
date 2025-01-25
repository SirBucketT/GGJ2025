using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public List<GameObject> objects;
    public int objectsToActivate = 5;

    private int currentIndex = 0;

    void Start()
    {
        foreach (var obj in objects)
        {
            obj.SetActive(false);
        }
        ActivateNextSet();
    }

    public void ActivateNextSet()
    {
        // Deactivate current set
        for (int i = 0; i < objectsToActivate; i++)
        {
            int index = (currentIndex + i) % objects.Count;
            objects[index].SetActive(false);
        }

        // Move to next set
        currentIndex = (currentIndex + objectsToActivate) % objects.Count;

        // Activate next set
        for (int i = 0; i < objectsToActivate; i++)
        {
            int index = (currentIndex + i) % objects.Count;
            objects[index].SetActive(true);
        }
    }
}