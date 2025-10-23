using UnityEngine;

public class HPRegenPassive : PassiveItems
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
        PlayerHealthController.instance.healthRegenRate = passiveItemStats[passiveItemLevel].hpRegenIncrease;
    }
}
