using UnityEngine;

public class PickupMagnet : MonoBehaviour
{
    [SerializeField] private float additionalMoveSpeed = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PickupExp[] expPickups = FindObjectsByType<PickupExp>(FindObjectsSortMode.None);

            foreach (PickupExp exp in expPickups)
            {
                exp.isMovingToPlayer = true;
                exp.moveSpeed += additionalMoveSpeed;
            }

            Destroy(gameObject);
        }
    }
}
