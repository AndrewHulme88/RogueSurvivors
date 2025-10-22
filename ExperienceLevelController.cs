using UnityEngine;
using System.Collections.Generic;

public class ExperienceLevelController : MonoBehaviour
{
    public static ExperienceLevelController instance;
    
    [SerializeField] private PickupExp pickupExp;
    [SerializeField] private List<Weapon> weaponsToUpgrade;
    [SerializeField] private List<PassiveItems> passiveItemsToUpgrade;
    [SerializeField] private float experienceMultiplier = 2f; // For scaling experience required per level

    public int currentExperience = 0;
    public List<int> expLevels;
    public int currentLevel = 1;
    public int levelCount = 100;

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
        while(expLevels.Count < levelCount)
        {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * experienceMultiplier));
        }
    }

    public void AddExperience(int amount)
    {
        currentExperience += (int)(amount * PlayerController.instance.expGain);
        
        if(currentExperience >= expLevels[currentLevel])
        {
            LevelUp();
        }

        UIController.instance.UpdateExperienceUI(currentExperience, expLevels[currentLevel], currentLevel);
        SFXManager.instance.PlaySoundEffectPitched(2);
    }

    public void SpawnExp(Vector3 position, int expValue)
    {
        Instantiate(pickupExp, position, Quaternion.identity).expAmount = expValue;
    }

    private void LevelUp()
    {
        currentExperience -= expLevels[currentLevel];
        currentLevel++;

        if (currentLevel >= expLevels.Count)
        {
            currentLevel = expLevels.Count - 1;
        }

        UIController.instance.levelUpPanel.SetActive(true);
        Time.timeScale = 0f;

        weaponsToUpgrade.Clear();
        passiveItemsToUpgrade.Clear();
        //List<Weapon> availableWeapons = new List<Weapon>();
        //List<PassiveItems> availablePassiveItems = new List<PassiveItems>();
        //availableWeapons.AddRange(PlayerController.instance.assignedWeapons);
        //availablePassiveItems.AddRange(PlayerController.instance.assignedPassiveItems);

        List<UpgradeOption> upgradePool = new List<UpgradeOption>();

        foreach(Weapon weaponOption in PlayerController.instance.assignedWeapons)
        {
            if(weaponOption.weaponLevel < weaponOption.weaponStats.Count - 1)
            {
                upgradePool.Add(new UpgradeOption(weaponOption));
            }
        }

        if(PlayerController.instance.assignedWeapons.Count + PlayerController.instance.maxLevelWeapons.Count < PlayerController.instance.maxWeapons)
        {
            foreach (Weapon weaponOption in PlayerController.instance.unassignedWeapons)
            {
                upgradePool.Add(new UpgradeOption(weaponOption));
            }
        }

        foreach(PassiveItems passiveItemOption in PlayerController.instance.assignedPassiveItems)
        {
            if (passiveItemOption.passiveItemLevel < passiveItemOption.passiveItemStats.Count - 1)
            {
                upgradePool.Add(new UpgradeOption(passiveItemOption));
            }
        }

        foreach (PassiveItems passiveItemOption in PlayerController.instance.unassignedPassiveItems)
        {
            upgradePool.Add(new UpgradeOption(passiveItemOption));
        }

        List<UpgradeOption> selectedUpgrades = new List<UpgradeOption>();
        int optionsToShow = Mathf.Min(3, upgradePool.Count);

        for (int i = 0; i < optionsToShow; i++)
        {
            int selectedIndex = Random.Range(0, upgradePool.Count);
            selectedUpgrades.Add(upgradePool[selectedIndex]);
            upgradePool.RemoveAt(selectedIndex);
        }

        for(int i = 0; i < UIController.instance.levelUpSelectionButtons.Length; i++)
        {
            if(i < selectedUpgrades.Count)
            {
                var upgrade = selectedUpgrades[i];
                UIController.instance.levelUpSelectionButtons[i].gameObject.SetActive(true);

                if (upgrade.isWeapon)
                {
                    UIController.instance.levelUpSelectionButtons[i].UpdateButtonDislay(upgrade.weaponOption);
                }
                else
                {
                    UIController.instance.levelUpSelectionButtons[i].UpdateButtonDislay(upgrade.passiveItemOption);
                }
            }
            else
            {
                UIController.instance.levelUpSelectionButtons[i].gameObject.SetActive(false);
            }
        }

        PlayerStatsController.instance.UpdateDisplay();



        //if (availableWeapons.Count > 0)
        //{
        //    int selectedWeapon = Random.Range(0, availableWeapons.Count);
        //    weaponsToUpgrade.Add(availableWeapons[selectedWeapon]);
        //    availableWeapons.RemoveAt(selectedWeapon);
        //}

        //if(PlayerController.instance.assignedWeapons.Count + PlayerController.instance.maxLevelWeapons.Count < PlayerController.instance.maxWeapons)
        //{
        //    availableWeapons.AddRange(PlayerController.instance.unassignedWeapons);
        //}

        //for (int i = weaponsToUpgrade.Count; i < PlayerController.instance.maxWeapons; i++)
        //{
        //    if (availableWeapons.Count > 0)
        //    {
        //        int selectedWeapon = Random.Range(0, availableWeapons.Count);
        //        weaponsToUpgrade.Add(availableWeapons[selectedWeapon]);
        //        availableWeapons.RemoveAt(selectedWeapon);
        //    }
        //}

        //for (int i = 0; i < weaponsToUpgrade.Count; i++)
        //{
        //    UIController.instance.levelUpSelectionButtons[i].UpdateButtonDislay(weaponsToUpgrade[i]);
        //}

        //for (int i = 0; i < UIController.instance.levelUpSelectionButtons.Length; i++)
        //{
        //    if(i < weaponsToUpgrade.Count)
        //    {
        //        UIController.instance.levelUpSelectionButtons[i].gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        UIController.instance.levelUpSelectionButtons[i].gameObject.SetActive(false);
        //    }
        //}

        //PlayerStatsController.instance.UpdateDisplay();
    }
}

public class UpgradeOption
{
    public Weapon weaponOption;
    public PassiveItems passiveItemOption;
    public bool isWeapon;

    public UpgradeOption(Weapon weapon)
    {
        weaponOption = weapon;
        isWeapon = true;
    }

    public UpgradeOption(PassiveItems passiveItem)
    {
        passiveItemOption = passiveItem;
        isWeapon = false;
    }
}
