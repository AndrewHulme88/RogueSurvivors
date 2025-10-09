using UnityEngine;
using System.Collections.Generic;

public class Weapon : MonoBehaviour
{
    public List<WeaponStats> weaponStats;
    public int weaponLevel = 0;

    [HideInInspector]
    public bool statsUpdated = false;

    public void LevelUp()
    {
        if(weaponLevel < weaponStats.Count - 1)
        {
            weaponLevel++;

            statsUpdated = true;
        }
    }
}

[System.Serializable]
public class WeaponStats
{
    public float projectileSpeed;
    public float damage;
    public float range;
    public float attackSpeed;
    public float quantity;
    public float duration;
}
