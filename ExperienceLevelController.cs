using UnityEngine;
using System.Collections.Generic;

public class ExperienceLevelController : MonoBehaviour
{
    public static ExperienceLevelController instance;
    
    [SerializeField] private PickupExp pickupExp;
    [SerializeField] private List<Weapon> weaponsToUpgrade;

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
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));
        }
    }

    public void AddExperience(int amount)
    {
        currentExperience += amount;
        
        if(currentExperience >= expLevels[currentLevel])
        {
            LevelUp();
        }

        UIController.instance.UpdateExperienceUI(currentExperience, expLevels[currentLevel], currentLevel);
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
        List<Weapon> availableWeapons = new List<Weapon>();
        availableWeapons.AddRange(PlayerController.instance.assignedWeapons);

        if (availableWeapons.Count > 0)
        {
            int selectedWeapon = Random.Range(0, availableWeapons.Count);
            weaponsToUpgrade.Add(availableWeapons[selectedWeapon]);
            availableWeapons.RemoveAt(selectedWeapon);
        }

        if(PlayerController.instance.assignedWeapons.Count + PlayerController.instance.maxLevelWeapons.Count < PlayerController.instance.maxWeapons)
        {
            availableWeapons.AddRange(PlayerController.instance.unassignedWeapons);
        }

        for (int i = weaponsToUpgrade.Count; i < PlayerController.instance.maxWeapons; i++)
        {
            if (availableWeapons.Count > 0)
            {
                int selectedWeapon = Random.Range(0, availableWeapons.Count);
                weaponsToUpgrade.Add(availableWeapons[selectedWeapon]);
                availableWeapons.RemoveAt(selectedWeapon);
            }
        }

        for (int i = 0; i < weaponsToUpgrade.Count; i++)
        {
            UIController.instance.levelUpSelectionButtons[i].UpdateButtonDislay(weaponsToUpgrade[i]);
        }

        for (int i = 0; i < UIController.instance.levelUpSelectionButtons.Length; i++)
        {
            if(i < weaponsToUpgrade.Count)
            {
                UIController.instance.levelUpSelectionButtons[i].gameObject.SetActive(true);
            }
            else
            {
                UIController.instance.levelUpSelectionButtons[i].gameObject.SetActive(false);
            }
        }
    }
}
