using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpSelectionButton : MonoBehaviour
{
    [SerializeField] private TMP_Text upgradeDescriptionText;
    [SerializeField] private TMP_Text nameLevelText;
    [SerializeField] private Image iconImage;

    private Weapon assignedWeapon;
    private PassiveItems assignedPassiveItem;
    private bool isWeapon;

    public void UpdateButtonDislay(Weapon weapon)
    {
        isWeapon = true;
        assignedWeapon = weapon;
        assignedPassiveItem = null;

        if (weapon.gameObject.activeSelf == true)
        {
            upgradeDescriptionText.text = weapon.weaponStats[weapon.weaponLevel].upgradeDescription;
            iconImage.sprite = weapon.weaponIcon;
            nameLevelText.text = "Upgrade: " + weapon.name;
        }
        else
        {
            upgradeDescriptionText.text = "Unlock " + weapon.name;
            iconImage.sprite = weapon.weaponIcon;
            nameLevelText.text = weapon.name;
        }

        assignedWeapon = weapon;
    }

    public void UpdateButtonDislay(PassiveItems passiveItem)
    {
        upgradeDescriptionText.text = "";
        nameLevelText.text = "";
        iconImage.sprite = null;

        isWeapon = false;
        assignedPassiveItem = passiveItem;
        assignedWeapon = null;

        if (passiveItem.gameObject.activeSelf == true)
        {
            upgradeDescriptionText.text = passiveItem.passiveItemStats[passiveItem.passiveItemLevel].upgradeDescription;
            iconImage.sprite = passiveItem.passiveItemIcon;
            nameLevelText.text = "Upgrade: " + passiveItem.name;
        }
        else
        {
            upgradeDescriptionText.text = "Unlock " + passiveItem.name;
            iconImage.sprite = passiveItem.passiveItemIcon;
            nameLevelText.text = passiveItem.name;
        }
        assignedPassiveItem = passiveItem;
    }

    public void SelectUpgrade()
    {
        if (isWeapon && assignedWeapon != null)
        {
            if (assignedWeapon.gameObject.activeSelf == true)
            {
                assignedWeapon.LevelUp();
            }
            else
            {
                PlayerController.instance.AddWeapon(assignedWeapon);
            }
                
        }
        else if(!isWeapon && assignedPassiveItem != null)
        {
            if(assignedPassiveItem.gameObject.activeSelf == true)
            {
                assignedPassiveItem.LevelUp();
            }
            else
            {
                PlayerController.instance.AddPassiveItem(assignedPassiveItem);
            }
        }

            UIController.instance.levelUpPanel.SetActive(false);
            Time.timeScale = 1f;
    }
}
