using System.Collections.Generic;
using UnityEngine;

public class PassiveItems : MonoBehaviour
{
    public List<PassiveItemStats> passiveItemStats;
    public int passiveItemLevel = 0;
    public Sprite passiveItemIcon;

    [HideInInspector]
    public bool statsUpdated = false;

    public void LevelUp()
    {
        if (passiveItemLevel < passiveItemStats.Count - 1)
        {
            passiveItemLevel++;

            statsUpdated = true;

            if (passiveItemLevel >= passiveItemStats.Count - 1)
            {
                PlayerController.instance.maxLevelPassiveItems.Add(this);
                PlayerController.instance.assignedPassiveItems.Remove(this);
            }
        }
    }
}

[System.Serializable]
public class PassiveItemStats
{
    public float moveSpeedIncrease;
    public string upgradeDescription;
}
