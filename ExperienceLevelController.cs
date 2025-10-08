using UnityEngine;

public class ExperienceLevelController : MonoBehaviour
{
    public static ExperienceLevelController instance;

    public int currentExperience = 0;

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

    public void AddExperience(int amount)
    {
        currentExperience += amount;
        // Optionally, you can add code here to handle level-ups or UI updates.
    }
}
