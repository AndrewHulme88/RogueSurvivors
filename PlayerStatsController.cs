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
