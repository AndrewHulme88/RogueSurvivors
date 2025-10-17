using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [SerializeField] Slider expLevelSlider;
    [SerializeField] TMP_Text expLevelText;
    [SerializeField] TMP_Text coinText;
    [SerializeField] TMP_Text timeText;
    [SerializeField] private InputActionReference startInput;
    [SerializeField] private TMP_Text sanityEffectNameText;
    [SerializeField] private TMP_Text sanityEffectDescriptionText;

    public TMP_Text endTimeText;
    public GameObject endGamePanel;
    public PlayerStatsUpgradeDisplay moveSpeedUpgradeDisplay;
    public PlayerStatsUpgradeDisplay healthUpgradeDisplay;
    public PlayerStatsUpgradeDisplay pickupRangeUpgradeDisplay;
    public PlayerStatsUpgradeDisplay maxWeaponsUpgradeDisplay;
    public GameObject levelUpPanel;
    public LevelUpSelectionButton[] levelUpSelectionButtons;
    public string mainMenuSceneName = "MainMenu";
    public GameObject pauseMenuPanel;
    public GameObject sanityEffectPanel;

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

    private void Update()
    {
        if (startInput.action.WasPressedThisFrame())
        {
            TogglePauseMenu();
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

    public void UpdateTimer(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

    public void TogglePauseMenu()
    {
        if (pauseMenuPanel.activeSelf == true)
        {
            pauseMenuPanel.SetActive(false);

            if(levelUpPanel.activeSelf == false)
            {
                Time.timeScale = 1f;
            }
        }
        else
        {
            pauseMenuPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ShowSanityEffectPanel(string name, string description)
    {
        sanityEffectNameText.text = name;
        sanityEffectDescriptionText.text = description;
        sanityEffectPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseSanityEffectPanel()
    {
        sanityEffectPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
