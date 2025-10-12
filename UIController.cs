using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [SerializeField] Slider expLevelSlider;
    [SerializeField] TMP_Text expLevelText;
    [SerializeField] TMP_Text coinText;

    public PlayerStatsUpgradeDisplay moveSpeedUpgradeDisplay;
    public PlayerStatsUpgradeDisplay healthUpgradeDisplay;
    public PlayerStatsUpgradeDisplay pickupRangeUpgradeDisplay;
    public PlayerStatsUpgradeDisplay maxWeaponsUpgradeDisplay;
    public GameObject levelUpPanel;
    public LevelUpSelectionButton[] levelUpSelectionButtons;

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

    public void UpdateExperienceUI(int currentExp, int levelExp, int currentLevel)
    {
        expLevelSlider.maxValue = levelExp;
        expLevelSlider.value = currentExp;
        expLevelText.text = "Level: " + currentLevel;
    }

    public void SkipLevelUp()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void UpdateCoins(int currentCoins)
    {
        coinText.text = "Coins: " + CoinController.instance.currentCoins;
    }

    public void PurchaseMoveSpeed()
    {
        PlayerStatsController.instance.PurchaseMoveSpeed();
        SkipLevelUp();
    }

    public void PurchaseMaxHealth()
    {
        PlayerStatsController.instance.PurchaseMaxHealth();
        SkipLevelUp();
    }

    public void PurchasePickupRange()
    {
        PlayerStatsController.instance.PurchasePickupRange();
        SkipLevelUp();
    }

    public void PurchaseMaxWeapons()
    {
        PlayerStatsController.instance.PurchaseMaxWeapons();
        SkipLevelUp();
    }
}
