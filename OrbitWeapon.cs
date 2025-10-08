using UnityEngine;

public class OrbitWeapon : MonoBehaviour
{
    [SerializeField] private float orbitSpeed = 10f;
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private Transform weaponToSpawn;
    [SerializeField] private float timeBetweenSpawns = 1f;

    private float spawnTimer;

    private void Update()
    {
        weaponHolder.transform.rotation = Quaternion.Euler(0f, 0f, weaponHolder.transform.rotation.eulerAngles.z + orbitSpeed * Time.deltaTime);

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            Instantiate(weaponToSpawn, weaponToSpawn.position, weaponToSpawn.rotation, weaponHolder).gameObject.SetActive(true);
            spawnTimer = timeBetweenSpawns;
        }
    }
}
