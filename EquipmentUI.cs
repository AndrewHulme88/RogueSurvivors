using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EquipmentUI : MonoBehaviour
{
    public static EquipmentUI instance;

    [SerializeField] private List<GameObject> slotParents;

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

    public void UpdateSlot(int slotIndex, Sprite equipmentIcon)
    {
        if (slotIndex < 0 || slotIndex >= slotParents.Count) return;

        Image slotImage = slotParents[slotIndex].GetComponent<Image>();

        if (slotImage != null)
        {
            slotImage.sprite = equipmentIcon;
            slotImage.enabled = true;
        }
    }
}
