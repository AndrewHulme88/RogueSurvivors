using UnityEngine;

public class AuraWeapon : Weapon
{
    [SerializeField] private DamageEnemy damageEnemy;

    private float spawnTime;
    private float spawnTimer;

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

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            spawnTimer = spawnTime;
            Instantiate(damageEnemy, transform.position, Quaternion.identity, transform).gameObject.SetActive(true);

            SFXManager.instance.PlaySoundEffectPitched(10);
        }
    }

    public void SetStats()
    {
        damageEnemy.damageAmount = weaponStats[weaponLevel].damage;
        transform.localScale = Vector3.one * weaponStats[weaponLevel].range;
        damageEnemy.damageInterval = weaponStats[weaponLevel].projectileSpeed;
        spawnTime = weaponStats[weaponLevel].cooldown;
        damageEnemy.lifetime = weaponStats[weaponLevel].duration;
        spawnTimer = 0f;
    }
}
