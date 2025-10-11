using UnityEngine;

public class WeaponThrower : Weapon
{
    [SerializeField] private DamageEnemy damageEnemy;

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

        if(throwTimer <= 0f)
        {
            throwTimer = weaponStats[weaponLevel].attackSpeed;

            for(int i = 0; i < weaponStats[weaponLevel].quantity; i++)
            {
                Instantiate(damageEnemy, damageEnemy.transform.position, damageEnemy.transform.rotation).gameObject.SetActive(true);
            }
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
