using UnityEngine;
using System.Collections.Generic;

public class DamageEnemy : MonoBehaviour
{
    [SerializeField] private float growthRate = 5f;
    [SerializeField] private bool shouldKnockBack = false;
    [SerializeField] private bool destroyParent = false;
    [SerializeField] private bool shouldDamageOverTime = false;
    [SerializeField] private bool destroyOnHit = false;

    public float damageInterval = 0.5f;
    public float damageAmount = 5f;
    public float lifetime = 2f;

    private Vector3 targetSize;
    private float damageTimer;
    private List<EnemyController> enemiesInRange = new List<EnemyController>();

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

        if (shouldDamageOverTime)
        {
            damageTimer -= Time.deltaTime;

            if(damageTimer <= 0f)
            {
                damageTimer = damageInterval;

                for (int i = 0; i < enemiesInRange.Count; i++)
                {
                    if (enemiesInRange[i] != null)
                    {
                        enemiesInRange[i].TakeDamage(damageAmount, shouldKnockBack);
                    }
                    else
                    {
                        enemiesInRange.RemoveAt(i);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (shouldDamageOverTime == false)
        {
            if (collision.CompareTag("Enemy"))
            {
                EnemyController enemy = collision.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damageAmount, shouldKnockBack);

                    if (destroyOnHit)
                    {
                        Destroy(gameObject);
                    }
                }
            }            
        }
        else
        {
            if (collision.CompareTag("Enemy"))
            {
                EnemyController enemy = collision.GetComponent<EnemyController>();
                if (enemy != null && !enemiesInRange.Contains(enemy))
                {
                    enemiesInRange.Add(enemy);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (shouldDamageOverTime)
        {
            if (collision.CompareTag("Enemy"))
            {
                EnemyController enemy = collision.GetComponent<EnemyController>();
                if (enemy != null && enemiesInRange.Contains(enemy))
                {
                    enemiesInRange.Remove(enemy);
                }
            }
        }
    }
}
