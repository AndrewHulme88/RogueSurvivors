using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float hitWaitTime = 1f;

    private Rigidbody2D rb;
    private Transform playerTransform;
    private float hitTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = PlayerHealthController.instance.transform;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed;
            if (rb.linearVelocity.x < 0f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (rb.linearVelocity.x > 0f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        if (hitTimer > 0f)
        {
            hitTimer -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && hitTimer <= 0f)
        {
            PlayerHealthController.instance.TakeDamage(damageAmount);

            hitTimer = hitWaitTime;
        }
    }
}
