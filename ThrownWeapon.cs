using UnityEngine;

public class ThrownWeapon : MonoBehaviour
{
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private float rotateSpeed = 20f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Throw();
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (rotateSpeed * 360f * Time.deltaTime * Mathf.Sign(PlayerController.instance.transform.localScale.x)));
    }

    private void Throw()
    {
        rb.linearVelocity = new Vector2(Random.Range(-throwForce, throwForce), throwForce);
    }
}
