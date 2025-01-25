using UnityEngine;

public class minimap : MonoBehaviour
{
    [SerializeField] Transform player;

    
    private Vector3 offset = new Vector3(0f, 30f, 0f);

    void LateUpdate()
    {
        if (player == null) return;
        
        Vector3 newPos = player.position + offset;
        newPos.y = offset.y;
        transform.position = newPos;

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}