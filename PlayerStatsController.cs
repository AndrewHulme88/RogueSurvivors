using UnityEngine;
using System.Collections.Generic;

public class PlayerStatsController : MonoBehaviour
{
    public static PlayerStatsController instance;

    [SerializeField] private List<PlayerStatsValue> moveSpeed;
    [SerializeField] private List<PlayerStatsValue> maxHealth;
    [SerializeField] private List<PlayerStatsValue> pickupRange;
    [SerializeField] private List<PlayerStatsValue> maxWeapons;
    [SerializeField] private int moveSpeedLevelCount = 10;
    [SerializeField] private int maxHealthLevelCount = 10;
    [SerializeField] private int pickupRangeLevelCount = 10;
    
    public int currentMoveSpeedLevel = 0;
    public int currentMaxHealthLevel = 0;
    public int currentPickupRangeLevel = 0;
    public int currentMaxWeaponsLevel = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(UIController.instance.levelUpPanel.activeSelf)
        {
            UpdateDisplay();
        }
    }

    private void Start()
    {
        for(int i = moveSpeed.Count - 1; i < moveSpeedLevelCount; i++)
        {
            moveSpeed.Add(new PlayerStatsValue(moveSpeed[i].cost + moveSpeed[1].cost, moveSpeed[i].value + (moveSpeed[1].value - moveSpeed[0].value)));
        }

        for (int i = maxHealth.Count - 1; i < maxHealthLevelCount; i++)
        {
            maxHealth.Add(new PlayerStatsValue(maxHealth[i].cost + maxHealth[1].cost, maxHealth[i].value + (maxHealth[1].value - maxHealth[0].value)));
        }

        for (int i = pickupRange.Count - 1; i < pickupRangeLevelCount; i++)
        {
            pickupRange.Add(new PlayerStatsValue(pickupRange[i].cost + pickupRange[1].cost, pickupRange[i].value + (pickupRange[1].value - pickupRange[0].value)));
        }
    }

    public void UpdateDisplay()
    {
        if(currentMoveSpeedLevel < moveSpeedLevelCount - 1)
        {
            UIController.instance.moveSpeedUpgradeDisplay.UpdateDisplay(moveSpeed[currentMoveSpeedLevel + 1].cost, moveSpeed[currentMoveSpeedLevel].value, moveSpeed[currentMoveSpeedLevel + 1].value);
        }
        else
        {
            UIController.instance.moveSpeedUpgradeDisplay.ShowMaxLevel();
        }

        if (currentMaxHealthLevel < maxHealthLevelCount - 1)
        {
            UIController.instance.healthUpgradeDisplay.UpdateDisplay(maxHealth[currentMaxHealthLevel + 1].cost, maxHealth[currentMaxHealthLevel].value, maxHealth[currentMaxHealthLevel + 1].value);
        }
        else
        {
            UIController.instance.healthUpgradeDisplay.ShowMaxLevel();
        }

        if (currentPickupRangeLevel < pickupRangeLevelCount - 1)
        {
            UIController.instance.pickupRangeUpgradeDisplay.UpdateDisplay(pickupRange[currentPickupRangeLevel + 1].cost, pickupRange[currentPickupRangeLevel].value, pickupRange[currentPickupRangeLevel + 1].value);
        }
        else
        {
            UIController.instance.pickupRangeUpgradeDisplay.ShowMaxLevel();
        }

        if (currentMaxWeaponsLevel < maxWeapons.Count - 1)
        {
            UIController.instance.maxWeaponsUpgradeDisplay.UpdateDisplay(maxWeapons[currentMaxWeaponsLevel + 1].cost, maxWeapons[currentMaxWeaponsLevel].value, maxWeapons[currentMaxWeaponsLevel + 1].value);
        }
        else
        {
            UIController.instance.maxWeaponsUpgradeDisplay.ShowMaxLevel();
        }
    }

    public void PurchaseMoveSpeed()
    {
        currentMoveSpeedLevel++;
        CoinController.instance.SpendCoins(moveSpeed[currentMoveSpeedLevel].cost);
        UpdateDisplay();
        PlayerController.instance.moveSpeed = moveSpeed[currentMoveSpeedLevel].value;
    }

    public void PurchaseMaxHealth()
    {
        currentMaxHealthLevel++;
        CoinController.instance.SpendCoins(maxHealth[currentMaxHealthLevel].cost);
        UpdateDisplay();
        PlayerHealthController.instance.maxHealth = maxHealth[currentMaxHealthLevel].value;
    }

    public void PurchasePickupRange()
    {
        currentPickupRangeLevel++;
        CoinController.instance.SpendCoins(pickupRange[currentPickupRangeLevel].cost);
        UpdateDisplay();
        PlayerController.instance.pickupRange = pickupRange[currentPickupRangeLevel].value;
    }

    public void PurchaseMaxWeapons()
    {
        currentMaxWeaponsLevel++;
        CoinController.instance.SpendCoins(maxWeapons[currentMaxWeaponsLevel].cost);
        UpdateDisplay();
        PlayerController.instance.maxWeapons = (int)maxWeapons[currentMaxWeaponsLevel].value;
    }
}

[System.Serializable]
public class PlayerStatsValue
{
    public int cost;
    public float value;

    public PlayerStatsValue(int newCost, float newValue)
    {
        cost = newCost;
        value = newValue;
    }
}
