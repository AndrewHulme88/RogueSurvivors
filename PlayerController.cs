using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference moveInput;
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocity = moveInput.action.ReadValue<Vector2>() * moveSpeed;

        if (rb.linearVelocity.x < 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (rb.linearVelocity.x > 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
