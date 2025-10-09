using UnityEngine;

public class OrbitWeapon : Weapon
{
    [SerializeField] private float orbitSpeed = 10f;
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private Transform weaponToSpawn;
    [SerializeField] private float timeBetweenSpawns = 1f;
    [SerializeField] private DamageEnemy damageEnemy;

    private float spawnTimer;

    private void Start()
    {
        SetStats();
    }

    private void Update()
    {
        weaponHolder.transform.rotation = Quaternion.Euler(0f, 0f, weaponHolder.transform.rotation.eulerAngles.z + orbitSpeed * Time.deltaTime * weaponStats[weaponLevel].projectileSpeed);

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            Instantiate(weaponToSpawn, weaponToSpawn.position, weaponToSpawn.rotation, weaponHolder).gameObject.SetActive(true);
            spawnTimer = timeBetweenSpawns;
        }

        if(statsUpdated)
        {
            statsUpdated = false;
            SetStats();
        }
    }

    public void SetStats()
    {
        damageEnemy.damageAmount = weaponStats[weaponLevel].damage;
        transform.localScale = Vector3.one * weaponStats[weaponLevel].range;
        timeBetweenSpawns = weaponStats[weaponLevel].attackSpeed;
        damageEnemy.lifetime = weaponStats[weaponLevel].duration;

        spawnTimer = 0f;
    }
}
