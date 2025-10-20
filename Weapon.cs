using UnityEngine;
using System.Collections.Generic;

public class Weapon : MonoBehaviour
{
    public List<WeaponStats> weaponStats;
    public int weaponLevel = 0;
    public Sprite weaponIcon;

    [HideInInspector]
    public bool statsUpdated = false;

    public void LevelUp()
    {
        if(weaponLevel < weaponStats.Count - 1)
        {
            weaponLevel++;

            statsUpdated = true;

            if(weaponLevel >= weaponStats.Count - 1)
            {
                PlayerController.instance.maxLevelWeapons.Add(this);
                PlayerController.instance.assignedWeapons.Remove(this);
            }
        }
    }
}

[System.Serializable]
public class WeaponStats
{
    public float projectileSpeed;
    public float damage;
    public float range;
    public float cooldown;
    public float quantity;
    public float duration;
    public string upgradeDescription;
}
