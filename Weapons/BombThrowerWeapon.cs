using UnityEngine;

public class BombThrowerWeapon : Weapon
{
    [SerializeField] private DamageEnemy damageEnemy;
    [SerializeField] private GameObject bombPrefab;

    private float throwTimer;

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

        throwTimer -= Time.deltaTime;

        if (throwTimer <= 0f)
        {
            throwTimer = weaponStats[weaponLevel].cooldown;

            for (int i = 0; i < weaponStats[weaponLevel].quantity; i++)
            {
                Instantiate(bombPrefab, bombPrefab.transform.position, bombPrefab.transform.rotation).gameObject.SetActive(true);                
            }

            SFXManager.instance.PlaySoundEffectPitched(4);
        }
    }

    public void SetStats()
    {
        damageEnemy.damageAmount = weaponStats[weaponLevel].damage;
        transform.localScale = Vector3.one * weaponStats[weaponLevel].range;
        damageEnemy.lifetime = weaponStats[weaponLevel].duration;

        throwTimer = 0f;
    }
}
