using UnityEngine;

public class PickupExp : MonoBehaviour
{
    [SerializeField] private float timeBetweenChecks = 0.2f;

    public float moveSpeed = 5f;
    public int expAmount = 10;
    public bool isMovingToPlayer = false;

    private float checkTimer = 0f;
    private PlayerController playerController;

    private void Start()
    {
        playerController = PlayerHealthController.instance.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(isMovingToPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerController.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            checkTimer -= Time.deltaTime;
            if (checkTimer <= 0f)
            {
                checkTimer = timeBetweenChecks;
                
                if (Vector3.Distance(transform.position, PlayerHealthController.instance.transform.position) <= playerController.pickupRange)
                {
                    isMovingToPlayer = true;
                    moveSpeed += playerController.moveSpeed;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ExperienceLevelController.instance.AddExperience(expAmount);
            Destroy(gameObject);
        }
    }
}
