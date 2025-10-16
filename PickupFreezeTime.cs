using UnityEngine;

public class PickupFreezeTime : MonoBehaviour
{
    [SerializeField] private float freezeDuration = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EnemyController[] enemies = FindObjectsByType<EnemyController>(FindObjectsSortMode.None);
            foreach (EnemyController enemy in enemies)
            {
                enemy.FreezeEnemy(freezeDuration);
            }

            EnemySpawner spawner = FindFirstObjectByType<EnemySpawner>();
            if (spawner != null)
            {
                spawner.FreezeTimer(freezeDuration);
            }

            Destroy(gameObject);
        }
    }
}
