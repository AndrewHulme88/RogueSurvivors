using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private GameObject deathParticles;

    public float maxHealth = 100f;

    private float currentHealth;

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
        if (PlayerController.instance.transform.localScale.x < 0.1f)
        {
            healthBarSlider.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (PlayerController.instance.transform.localScale.x > 0.1f)
        {
            healthBarSlider.transform.localScale = new Vector3(1f, 1f, 1f);
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

    private void Die()
    {
        SFXManager.instance.PlaySoundEffect(3);
        gameObject.SetActive(false);
        LevelManager.instance.EndGame();
        Instantiate(deathParticles, transform.position, Quaternion.identity);
    }
}
