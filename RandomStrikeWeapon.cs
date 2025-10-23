using UnityEngine;
using System.Collections.Generic;

public class RandomStrikeWeapon : Weapon
{
    [SerializeField] private DamageEnemy damageEnemy;
    [SerializeField] private GameObject strikePrefab;
    [SerializeField] private LayerMask enemyLayers;

    public float weaponRange = 10f;

    private float strikeTimer;

    private void Start()
    {
        SetStats();
    }

    private void Update()
    {
        strikeTimer -= Time.deltaTime;

        if(strikeTimer <= 0f)
        {
            StrikeRandomEnemies((int)weaponStats[weaponLevel].quantity);
            strikeTimer = weaponStats[weaponLevel].cooldown;
        }
    }

    private void StrikeRandomEnemies(int quantity)
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, weaponRange * weaponStats[weaponLevel].range, enemyLayers);

        for (int i = 0; i < quantity; i++)
        {
            if (enemiesInRange.Length <= 0f)
                return;
            int randomIndex = Random.Range(0, enemiesInRange.Length);
            GameObject targetEnemy = enemiesInRange[randomIndex].gameObject;
            Vector3 strikePosition = targetEnemy.transform.position;
            Instantiate(strikePrefab, strikePosition, Quaternion.identity).gameObject.SetActive(true);
        }
        SFXManager.instance.PlaySoundEffectPitched(4);
    }

    public void SetStats()
    {
        damageEnemy.damageAmount = weaponStats[weaponLevel].damage;
        transform.localScale = Vector3.one * weaponStats[weaponLevel].range;
        damageEnemy.lifetime = weaponStats[weaponLevel].duration;

        strikeTimer = weaponStats[weaponLevel].cooldown;
    }
}
