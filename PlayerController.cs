using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference moveInput;
    [SerializeField] private Transform sprite;

    public float moveSpeed = 5f;
    public float pickupRange = 1.5f;

    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
}
