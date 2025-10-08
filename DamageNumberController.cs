using System.Collections.Generic;
using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    public static DamageNumberController instance;

    public DamageNumber numberToSpawn;
    public Transform canvasTransform;

    private List<DamageNumber> activeNumbers = new List<DamageNumber>();

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

    public void SpawnDamageNumber(float damageAmount, Vector3 position)
    {
        if (numberToSpawn != null && canvasTransform != null)
        {
            int roundedNumber = Mathf.RoundToInt(damageAmount);
            DamageNumber newDamageNumber = GetFromActiveNumbers();
            newDamageNumber.SetDamageAmount(roundedNumber);
            newDamageNumber.gameObject.SetActive(true);

            newDamageNumber.transform.position = position;
        }
    }

    public DamageNumber GetFromActiveNumbers()
    {
        DamageNumber numberToOutput = null;

        if(activeNumbers.Count == 0)
        {
            numberToOutput = Instantiate(numberToSpawn, canvasTransform);
        }
        else
        {
            numberToOutput = activeNumbers[0];
            activeNumbers.RemoveAt(0);
        }

        return numberToOutput;
    }

    public void PlaceInPool(DamageNumber numberToPlace)
    {
        numberToPlace.gameObject.SetActive(false);
        activeNumbers.Add(numberToPlace);
    }
}
