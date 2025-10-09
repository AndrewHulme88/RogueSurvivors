using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    [SerializeField] private float growthRate = 5f;
    [SerializeField] private bool shouldKnockBack = false;
    [SerializeField] private bool destroyParent = false;

    public float damageAmount = 5f;
    public float lifetime = 2f;

    private Vector3 targetSize;

    private void Start()
    {
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growthRate * Time.deltaTime);
        lifetime -= Time.deltaTime;

        if (lifetime <= 0f)
        {
            targetSize = Vector3.zero;

            if (transform.localScale.x <= 0f)
            {
                Destroy(gameObject);

                if (destroyParent && transform.parent != null)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damageAmount, shouldKnockBack);
            }
        }
    }
}
