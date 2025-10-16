using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShrineController : MonoBehaviour
{
    [SerializeField] private Slider shrineSlider;
    [SerializeField] private float fillRate = 20f;
    [SerializeField] private float sanityRestoreAmount = 100f;
    [SerializeField] private GameObject textObject;
    [SerializeField] private float textLifetime = 3f;
    [SerializeField] private float textFloatSpeed = 1f;

    private bool playerInRange = false;
    private bool hasActivated = false;
    private float maxFill = 100f;
    private float currentFill = 0f;

    private void Start()
    {
        shrineSlider.maxValue = maxFill;
        shrineSlider.value = currentFill;
        shrineSlider.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(playerInRange)
        {
            shrineSlider.gameObject.SetActive(true);
            currentFill += Time.deltaTime * fillRate;
        }
        else
        {
            shrineSlider.gameObject.SetActive(false);
            currentFill = 0f;
        }

        currentFill = Mathf.Clamp(currentFill, 0, maxFill);
        shrineSlider.value = currentFill;

        if (currentFill >= maxFill)
        {
            SanityController.instance.RestoreSanity(sanityRestoreAmount);
            hasActivated = true;
            StartCoroutine(DestroyDelay());
        }

        if (hasActivated)
        {
            textObject.SetActive(true);
            textObject.transform.position += Vector3.up * textFloatSpeed * Time.deltaTime;
        }
    }

    IEnumerator DestroyDelay()
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        shrineSlider.gameObject.SetActive(false);
        yield return new WaitForSeconds(textLifetime);
        textObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
