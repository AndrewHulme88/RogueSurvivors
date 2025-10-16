using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float hitWaitTime = 1f;
    [SerializeField] private float health = 5f;
    [SerializeField] private float knockBackTime = 0.2f;
    [SerializeField] private int expAmount = 1;
    [SerializeField] private int coinValue = 1;
    [SerializeField] private float coinDropChance = 0.5f;

    private Rigidbody2D rb;
    private Transform playerTransform;
    private float hitTimer = 0f;
    private float knockBackTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = PlayerHealthController.instance.transform;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            if (knockBackTimer > 0f)
            {
                knockBackTimer -= Time.deltaTime;
                
                if(moveSpeed > 0f)
                {
                    moveSpeed = -moveSpeed * 2f;
                }

                if (knockBackTimer <= 0f)
                {
                    moveSpeed = -moveSpeed / 2f;
                }
            }

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

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Destroy(gameObject);

            ExperienceLevelController.instance.SpawnExp(transform.position, expAmount);

            if (Random.value <= coinDropChance)
            {
                CoinController.instance.DropCoin(transform.position, coinValue);
            }

            SFXManager.instance.PlaySoundEffectPitched(0);
        }
        else
        {
            SFXManager.instance.PlaySoundEffectPitched(1);
        }

        DamageNumberController.instance.SpawnDamageNumber(damage, transform.position);
    }

    public void TakeDamage(float damage, bool shouldKnockBack)
    {
        TakeDamage(damage);

        if (shouldKnockBack)
        {
            knockBackTimer = knockBackTime;
            
        }
    }

    public void KillEnemy()
    {
        Destroy(gameObject);

        ExperienceLevelController.instance.SpawnExp(transform.position, expAmount);

        if (Random.value <= coinDropChance)
        {
            CoinController.instance.DropCoin(transform.position, coinValue);
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
