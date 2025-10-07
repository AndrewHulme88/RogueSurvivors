using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform playerTransform;

    private void Start()
    {
        GameObject player = FindFirstObjectByType<PlayerController>()?.gameObject;
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    private void LateUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 newPosition = playerTransform.position;
            newPosition.z = transform.position.z; 
            transform.position = newPosition;
        }
    }
}
