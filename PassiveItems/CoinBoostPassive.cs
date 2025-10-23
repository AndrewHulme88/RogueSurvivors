using UnityEngine;

public class CoinBoostPassive : PassiveItems
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
        CoinController.instance.coinGainMultiplier = passiveItemStats[passiveItemLevel].coinGainIncrease;
    }
}
