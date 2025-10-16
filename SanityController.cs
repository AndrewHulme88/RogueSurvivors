using UnityEngine;
using UnityEngine.UI;

public class SanityController : MonoBehaviour
{
    public static SanityController instance;

    [SerializeField] private Slider sanityBarSlider;

    public float maxSanity = 100f;
    public float sanityDecreaseRate = 2f;

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
    }

    private void Update()
    {
        currentSanity -= Time.deltaTime * sanityDecreaseRate;

        currentSanity = Mathf.Clamp(currentSanity, 0, maxSanity);
        sanityBarSlider.value = currentSanity;

        if (currentSanity <= 0)
        {
            
        }
    }

    public void RestoreSanity(float amount)
    {
        currentSanity += amount;
        currentSanity = Mathf.Clamp(currentSanity, 0, maxSanity);
        sanityBarSlider.value = currentSanity;
    }
}
