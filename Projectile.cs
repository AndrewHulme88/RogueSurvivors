using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed = 10f;
    public bool isBouncingProjectile = false;
    public bool doesPassThroughEnemies = false;
    public int remainingBounces = 1;

    private void Update()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isBouncingProjectile && remainingBounces > 0)
        {
            if (!doesPassThroughEnemies && collision.CompareTag("Enemy"))
            {
                float randomAngle = Random.Range(0f, 360f);
                transform.rotation = Quaternion.Euler(0f, 0f, randomAngle);
            }

            remainingBounces--;
        }
        else if(isBouncingProjectile && remainingBounces <= 0)
        {
            Destroy(gameObject);
        }
    }
}
