using UnityEngine;
using TMPro;

public class PlayerStatsUpgradeDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text valueText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private GameObject upgradeButton;

    public void UpdateDisplay(int cost, float oldValue, float newValue)
    {
        valueText.text = oldValue.ToString("F1") + " -> " + newValue.ToString("F1");
        costText.text = "Cost: " + cost;

        if (CoinController.instance.currentCoins >= cost)
        {
            upgradeButton.SetActive(true);
        }
        else
        {
            upgradeButton.SetActive(false);
        }
    }

    public void ShowMaxLevel()
    {
        valueText.text = "Max Level";
        costText.text = "";
        upgradeButton.SetActive(false);
    }
}
