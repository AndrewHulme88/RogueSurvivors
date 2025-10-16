using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed = 10f;

    private void Update()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }
}
