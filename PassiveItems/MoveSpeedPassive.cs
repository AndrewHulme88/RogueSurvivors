using UnityEngine;

public class MoveSpeedPassive : PassiveItems
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
        PlayerController.instance.moveSpeed += passiveItemStats[passiveItemLevel].moveSpeedIncrease;
    }
}
