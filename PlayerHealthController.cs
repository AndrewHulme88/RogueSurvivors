using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private GameObject deathParticles;

    public float maxHealth = 100f;
    public float currentHealth;
    public float healthRegenRate = 0f;

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
        currentHealth = maxHealth;
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;
    }

    private void Update()
    {
        if (currentHealth < maxHealth && healthRegenRate > 0f)
        {
            Heal(healthRegenRate * Time.deltaTime);
        }
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBarSlider.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Player Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }

        healthBarSlider.value = currentHealth;
    }

    public void UpdateHealthUI()
    {
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;
    }

    private void Die()
    {
        SFXManager.instance.PlaySoundEffect(3);
        gameObject.SetActive(false);
        LevelManager.instance.EndGame();
        Instantiate(deathParticles, transform.position, Quaternion.identity);
    }
}
