using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private float lifetime = 0.5f;
    [SerializeField] private float floatSpeed = 1f;

    private float lifeTimer;

    private void Start()
    {
        lifeTimer = lifetime;
    }

    private void Update()
    {
        if (lifeTimer > 0f)
        {
            lifeTimer -= Time.deltaTime;

            if (lifeTimer <= 0f)
            {
                DamageNumberController.instance.PlaceInPool(this);
            }
        }

        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
    }

    public void SetDamageAmount(int damageDisplay)
    {
        lifeTimer = lifetime;

        if (damageText != null)
        {
            damageText.text = damageDisplay.ToString();
        }
    }
}
