using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Unity.Jobs;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private InputActionReference moveInput;

    public string characterName;
    public Sprite characterSprite;
    public GameObject spriteObject;
    public List<Weapon> unassignedWeapons;
    public List<Weapon> assignedWeapons;
    public List<PassiveItems> unassignedPassiveItems;
    public List<PassiveItems> assignedPassiveItems;
    public bool isMovementReversed = false;
    // Stats
    public float moveSpeed = 5f;
    public float pickupRange = 1f;
    public int maxWeapons = 3;
    public int maxPassiveItems = 3;
    public float maxSanity = 100f;
    public float sanityDecreaseRate = 2f;
    public int expGain = 1;

    [HideInInspector]
    public List<Weapon> maxLevelWeapons = new List<Weapon>();
    public List<PassiveItems> maxLevelPassiveItems = new List<PassiveItems>();

    private Rigidbody2D rb;
    private Animator anim;

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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = spriteObject.GetComponent<Animator>();

        if(assignedWeapons.Count == 0)
        {
            AddWeapon(Random.Range(0, unassignedWeapons.Count));
        }

        EquipmentUI.instance.UpdateSlot(assignedWeapons.Count - 1, assignedWeapons[assignedWeapons.Count - 1].weaponIcon);
    }

    void Update()
    {
        // Movement
        Vector2 input = moveInput.action.ReadValue<Vector2>();
        if (isMovementReversed)
        {
            input = -input;
        }

        rb.linearVelocity = input * moveSpeed;

        if (rb.linearVelocity.x < 0f)
        {
            spriteObject.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (rb.linearVelocity.x > 0f)
        {
            spriteObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        // Animation
        if(anim != null)
        {
            if(rb.linearVelocity != Vector2.zero)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
        }
    }

    public void AddWeapon(int weaponNumber)
    {
        if(weaponNumber < unassignedWeapons.Count)
        {
            assignedWeapons.Add(unassignedWeapons[weaponNumber]);
            unassignedWeapons[weaponNumber].gameObject.SetActive(true);
            unassignedWeapons.RemoveAt(weaponNumber);
            EquipmentUI.instance.UpdateSlot(assignedWeapons.Count - 1, assignedWeapons[assignedWeapons.Count - 1].weaponIcon);
        }
    }

    public void AddWeapon(Weapon weaponToAdd)
    {
        weaponToAdd.gameObject.SetActive(true);
        assignedWeapons.Add(weaponToAdd);
        unassignedWeapons.Remove(weaponToAdd);
        EquipmentUI.instance.UpdateSlot(assignedWeapons.Count - 1, assignedWeapons[assignedWeapons.Count - 1].weaponIcon);
    }

    public void AddPassiveItem(int itemNumber)
    {
        if (itemNumber < unassignedPassiveItems.Count)
        {
            assignedPassiveItems.Add(unassignedPassiveItems[itemNumber]);
            unassignedPassiveItems[itemNumber].gameObject.SetActive(true);
            unassignedPassiveItems.RemoveAt(itemNumber);
        }
    }

    public void AddPassiveItem(PassiveItems itemToAdd)
    {
        itemToAdd.gameObject.SetActive(true);
        assignedPassiveItems.Add(itemToAdd);
        unassignedPassiveItems.Remove(itemToAdd);
    }
}
