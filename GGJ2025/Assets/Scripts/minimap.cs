using UnityEngine;

public class minimap : MonoBehaviour
{
    [SerializeField] Transform player;

    
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    private void LateUpdate()
    {
        if (player == null) return;
        
        Vector3 newPosition = player.position + offset;
        

        transform.position = newPosition;
        transform.rotation = Quaternion.identity;
    }
}