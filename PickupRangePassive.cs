using UnityEngine;

public class PickupRangePassive : PassiveItems
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
        PlayerController.instance.pickupRange += passiveItemStats[passiveItemLevel].pickupRangeIncrease;
    }
}
