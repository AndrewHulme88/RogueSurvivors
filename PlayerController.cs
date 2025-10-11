using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Unity.Jobs;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private InputActionReference moveInput;
    [SerializeField] private Transform sprite;

    public float moveSpeed = 5f;
    public float pickupRange = 1f;
    public int maxWeapons = 3;
    public List<Weapon> unassignedWeapons;
    public List<Weapon> assignedWeapons;

    [HideInInspector]
    public List<Weapon> maxLevelWeapons = new List<Weapon>();

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
        anim = GetComponent<Animator>();

        if(assignedWeapons.Count == 0)
        {
            AddWeapon(Random.Range(0, unassignedWeapons.Count));
        }
    }

    void Update()
    {
        // Movement
        rb.linearVelocity = moveInput.action.ReadValue<Vector2>() * moveSpeed;

        if (rb.linearVelocity.x < 0f)
        {
            sprite.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (rb.linearVelocity.x > 0f)
        {
            sprite.transform.localScale = new Vector3(-1f, 1f, 1f);
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
        }
    }

    public void AddWeapon(Weapon weaponToAdd)
    {
         weaponToAdd.gameObject.SetActive(true);
        assignedWeapons.Add(weaponToAdd);
        unassignedWeapons.Remove(weaponToAdd);
    }
}
