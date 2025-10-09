using UnityEngine;
using System.Collections.Generic;

public class ExperienceLevelController : MonoBehaviour
{
    public static ExperienceLevelController instance;
    
    [SerializeField] private PickupExp pickupExp;

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
        UIController.instance.levelUpSelectionButtons[0].UpdateButtonDislay(PlayerController.instance.assignedWeapons[0]);
        UIController.instance.levelUpSelectionButtons[1].UpdateButtonDislay(PlayerController.instance.unassignedWeapons[0]);
        UIController.instance.levelUpSelectionButtons[2].UpdateButtonDislay(PlayerController.instance.unassignedWeapons[1]);
    }
}
