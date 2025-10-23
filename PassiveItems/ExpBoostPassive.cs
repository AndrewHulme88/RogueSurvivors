using UnityEngine;

public class ExpBoostPassive : PassiveItems
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
        PlayerController.instance.expGain *= passiveItemStats[passiveItemLevel].expGainIncrease;
    }
}
