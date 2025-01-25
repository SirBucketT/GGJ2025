using UnityEngine;

public class RandomObjectSpawner : MonoBehaviour 
{
    public GameObject[] myObjects;
    public Vector2 spawnAreaSize = new Vector2(20f, 20f);
    public float spawnInterval = 5f;
    public float spawnZ = -1f;
    public LayerMask collisionLayers;

    private float timer = 0f;

    void Update() 
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval) 
        {
            SpawnRandomObject();
            timer = 0f;
        }
    }

    void SpawnRandomObject()
    {
        int randomIndex = Random.Range(0, myObjects.Length);
        Vector2 randomSpawnPosition;
        int attempts = 0;
        
        do 
        {
            randomSpawnPosition = new Vector2(
                Random.Range(-spawnAreaSize.x/2, spawnAreaSize.x/2), 
                Random.Range(-spawnAreaSize.y/2, spawnAreaSize.y/2)
            );
            attempts++;

            if (attempts > 50) 
            {
                Debug.LogWarning("Could not find non-colliding spawn point");
                return;
            }
        } 
        while (Physics2D.OverlapCircle(randomSpawnPosition, 0.5f, collisionLayers) != null);

        Vector3 spawnPosition = new Vector3(randomSpawnPosition.x, randomSpawnPosition.y, spawnZ);
        Instantiate(myObjects[randomIndex], spawnPosition, Quaternion.identity);
    }
}   