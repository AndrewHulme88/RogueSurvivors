using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EquipmentUI : MonoBehaviour
{
    public static EquipmentUI instance;

    [SerializeField] private List<GameObject> weaponSlotParents;
    [SerializeField] private List<GameObject> passiveSlotParents;

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

    public void UpdateWeaponSlot(int slotIndex, Sprite equipmentIcon)
    {
        if (slotIndex < 0 || slotIndex >= weaponSlotParents.Count) return;

        Image slotImage = weaponSlotParents[slotIndex].GetComponent<Image>();

        if (slotImage != null)
        {
            slotImage.sprite = equipmentIcon;
            slotImage.enabled = true;
        }
    }

    public void UpdatePassiveSlot(int slotIndex, Sprite equipmentIcon)
    {
        if (slotIndex < 0 || slotIndex >= passiveSlotParents.Count) return;
        Image slotImage = passiveSlotParents[slotIndex].GetComponent<Image>();
        if (slotImage != null)
        {
            slotImage.sprite = equipmentIcon;
            slotImage.enabled = true;
        }
    }
}
