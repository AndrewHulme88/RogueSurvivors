using UnityEngine;

public class OrbitWeapon : MonoBehaviour
{
    [SerializeField] private float orbitSpeed = 10f;
    [SerializeField] private Transform weaponHolder;

    private void Update()
    {
        weaponHolder.transform.rotation = Quaternion.Euler(0f, 0f, weaponHolder.transform.rotation.eulerAngles.z + orbitSpeed * Time.deltaTime);
    }
}
