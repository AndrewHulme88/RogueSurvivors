using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private DamageEnemy damageEnemy;

    private float attackTimer;
    private float facingDirection;

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

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            attackTimer = weaponStats[weaponLevel].attackSpeed;

            if (PlayerController.instance.sprite.transform.localScale.x > 0.1f)
            {
                facingDirection = -1f;
                damageEnemy.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            }
            else if (PlayerController.instance.sprite.transform.localScale.x < 0.1f)
            {
                facingDirection = 1f;
                damageEnemy.transform.rotation = Quaternion.identity;
            }

            Instantiate(damageEnemy, damageEnemy.transform.position, damageEnemy.transform.rotation, transform).gameObject.SetActive(true);

            for (int i = 1; i < weaponStats[weaponLevel].quantity; i++)
            {
                float rotationAngle = (360f / weaponStats[weaponLevel].quantity) * i;
                Instantiate(damageEnemy, damageEnemy.transform.position, Quaternion.Euler(0f, 0f, damageEnemy.transform.rotation.eulerAngles.z + rotationAngle), transform).gameObject.SetActive(true);
            }
        }
    }

    public void SetStats()
    {
        damageEnemy.damageAmount = weaponStats[weaponLevel].damage;
        transform.localScale = Vector3.one * weaponStats[weaponLevel].range;
        damageEnemy.lifetime = weaponStats[weaponLevel].duration;
        attackTimer = 0f;
    }
}
