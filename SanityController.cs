using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SanityController : MonoBehaviour
{
    public static SanityController instance;

    [SerializeField] private Slider sanityBarSlider;

    public float maxSanity = 100f;
    public float sanityDecreaseRate = 2f;

    private EnemySpawner enemySpawner;
    private float currentSanity;

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
        currentSanity = maxSanity;
        sanityBarSlider.maxValue = maxSanity;
        sanityBarSlider.value = currentSanity;
        enemySpawner = FindFirstObjectByType<EnemySpawner>();
    }

    private void Update()
    {
        currentSanity -= Time.deltaTime * sanityDecreaseRate;

        currentSanity = Mathf.Clamp(currentSanity, 0, maxSanity);
        sanityBarSlider.value = currentSanity;

        if (currentSanity <= 0)
        {
            ApplyRandomSanityDebuff();
        }
    }

    public void RestoreSanity(float amount)
    {
        currentSanity += amount;
        currentSanity = Mathf.Clamp(currentSanity, 0, maxSanity);
        sanityBarSlider.value = currentSanity;
    }

    private void ApplyRandomSanityDebuff()
    {
        List<SanityEffect> sanityEffectPool = new List<SanityEffect> {
            new SanityEffect { effectName = "Panic", description = "Enemies spawn faster", effectAction = () => {enemySpawner.spawnTimeMultiplier *= 1.2f; } },
            new SanityEffect { effectName = "Hallucination", description = "Controls are reversed", effectAction = () => { PlayerController.instance.isMovementReversed = true; } },
            new SanityEffect { effectName = "Weakness", description = "Max health is reduced", effectAction = () => { PlayerHealthController.instance.maxHealth *= 0.9f; PlayerHealthController.instance.currentHealth = Mathf.Min(PlayerHealthController.instance.currentHealth, PlayerHealthController.instance.maxHealth); PlayerHealthController.instance.UpdateHealthUI(); } },
            new SanityEffect { effectName = "Clumsiness", description = "Movement speed is reduced", effectAction = () => { PlayerController.instance.moveSpeed *= 0.9f; } },
            new SanityEffect { effectName = "Confusion", description = "Pickup range is reduced", effectAction = () => { PlayerController.instance.pickupRange *= 0.8f; } },
            new SanityEffect { effectName = "Stress", description = "Sanity decreases faster", effectAction = () => { sanityDecreaseRate *= 1.5f; } },
        };

        SanityEffect debuff = sanityEffectPool[UnityEngine.Random.Range(0, sanityEffectPool.Count)];
        debuff.effectAction.Invoke();
        currentSanity = maxSanity;
        UIController.instance.ShowSanityEffectPanel(debuff.effectName, debuff.description);
    }
}

public class SanityEffect
{
    public string effectName;
    public string description;
    public Action effectAction;
}
