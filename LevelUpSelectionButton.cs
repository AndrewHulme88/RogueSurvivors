using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpSelectionButton : MonoBehaviour
{
    [SerializeField] private TMP_Text upgradeDescriptionText;
    [SerializeField] private TMP_Text nameLevelText;
    [SerializeField] private Image weaponIconImage;

    private Weapon assignedWeapon;

    public void UpdateButtonDislay(Weapon weapon)
    {
        if (weapon.gameObject.activeSelf == true)
        {
            upgradeDescriptionText.text = weapon.weaponStats[weapon.weaponLevel].upgradeDescription;
            weaponIconImage.sprite = weapon.weaponIcon;
            nameLevelText.text = weapon.name + " - Lvl. " + weapon.weaponLevel;
        }
        else
        {
            upgradeDescriptionText.text = "Unlock " + weapon.name;
            weaponIconImage.sprite = weapon.weaponIcon;
            nameLevelText.text = weapon.name;
        }

        assignedWeapon = weapon;
    }

    public void SelectUpgrade()
    {
        if (assignedWeapon != null)
        {
            if (assignedWeapon.gameObject.activeSelf == true)
            {
                assignedWeapon.LevelUp();
            }
            else
            {
                PlayerController.instance.AddWeapon(assignedWeapon);
            }
                
            UIController.instance.levelUpPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
