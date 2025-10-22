using UnityEngine;

public class MaxHealthPassive : PassiveItems
{
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
    }

    public void SetStats()
    {
        PlayerHealthController.instance.maxHealth += passiveItemStats[passiveItemLevel].maxHealthIncrease;
    }
}
