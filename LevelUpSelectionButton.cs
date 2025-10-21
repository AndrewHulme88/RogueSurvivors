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
        upgradeDescriptionText.text = "";
        nameLevelText.text = "";
        //iconImage.sprite = null;

        isWeapon = true;
        assignedWeapon = weapon;
        assignedPassiveItem = null;

        if (weapon == null)
        {
            Debug.LogError(" Weapon is null when trying to update button!");
            return;
        }

        if (upgradeDescriptionText == null || nameLevelText == null || iconImage == null)
        {
            Debug.LogError($" UI references not assigned on {gameObject.name}");
            return;
        }

        if (weapon.weaponStats == null || weapon.weaponStats.Count == 0)
        {
            Debug.LogError($" Weapon {weapon.name} has no weaponStats assigned!");
            return;
        }

        if (weapon.weaponLevel >= weapon.weaponStats.Count)
        {
            Debug.LogError($" Weapon level {weapon.weaponLevel} exceeds stats count ({weapon.weaponStats.Count}) on {weapon.name}");
            return;
        }

        if (weapon.gameObject.activeSelf == true)
        {
            //upgradeDescriptionText.text = weapon.weaponStats[weapon.weaponLevel].upgradeDescription;
            ////iconImage.sprite = weapon.weaponIcon;
            //nameLevelText.text = weapon.name + " - Lvl. " + weapon.weaponLevel;
            upgradeDescriptionText.text = weapon.weaponStats[weapon.weaponLevel].upgradeDescription;
            nameLevelText.text = $"{weapon.name} - Lvl. {weapon.weaponLevel}";
            iconImage.sprite = weapon.weaponIcon;
        }
        else
        {
            //upgradeDescriptionText.text = "Unlock " + weapon.name;
            ////iconImage.sprite = weapon.weaponIcon;
            //nameLevelText.text = weapon.name;
            upgradeDescriptionText.text = $"Unlock {weapon.name}";
            nameLevelText.text = weapon.name;
            iconImage.sprite = weapon.weaponIcon;
        }

        assignedWeapon = weapon;
        //iconImage.sprite = weapon.weaponIcon;
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
            //upgradeDescriptionText.text = passiveItem.passiveItemStats[passiveItem.passiveItemLevel].upgradeDescription;
            ////iconImage.sprite = passiveItem.passiveItemIcon;
            //nameLevelText.text = passiveItem.name + " - Lvl. " + passiveItem.passiveItemLevel;
            upgradeDescriptionText.text = passiveItem.passiveItemStats[passiveItem.passiveItemLevel].upgradeDescription;
            nameLevelText.text = $"{passiveItem.name} - Lvl. {passiveItem.passiveItemLevel}";
            iconImage.sprite = passiveItem.passiveItemIcon;
        }
        else
        {
            //upgradeDescriptionText.text = "Unlock " + passiveItem.name;
            ////iconImage.sprite = passiveItem.passiveItemIcon;
            //nameLevelText.text = passiveItem.name;
            upgradeDescriptionText.text = $"Unlock {passiveItem.name}";
            nameLevelText.text = passiveItem.name;
            iconImage.sprite = passiveItem.passiveItemIcon;
        }
        assignedPassiveItem = passiveItem;
        //iconImage.sprite = passiveItem.passiveItemIcon;
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
