using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] private DamageEnemy damageEnemy;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private LayerMask enemyLayers;

    public float weaponRange = 10f;

    private float shotTimer;

    private void Start()
    {
        SetStats();
    }

    private void Update()
    {
        if (statsUpdated)
        {
            statsUpdated = false;
            SetStats();
        }

        shotTimer -= Time.deltaTime;

        if(shotTimer <= 0f)
        {
            shotTimer = weaponStats[weaponLevel].attackSpeed;

            Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, weaponRange * weaponStats[weaponLevel].range, enemyLayers);

            if(enemiesInRange.Length > 0)
            {
                for (int i = 0; i < weaponStats[weaponLevel].quantity; i++)
                {
                    Vector3 targetPosition = enemiesInRange[Random.Range(0, enemiesInRange.Length)].transform.position;
                    Vector3 direction = targetPosition - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    angle -= 90f;
                    projectilePrefab.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                    Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation).gameObject.SetActive(true);
                }
            }
        }
    }

    public void SetStats()
    {
        damageEnemy.damageAmount = weaponStats[weaponLevel].damage;
        transform.localScale = Vector3.one * weaponStats[weaponLevel].range;
        damageEnemy.lifetime = weaponStats[weaponLevel].duration;
        shotTimer = 0f;
        projectilePrefab.moveSpeed = weaponStats[weaponLevel].projectileSpeed;
    }
}
