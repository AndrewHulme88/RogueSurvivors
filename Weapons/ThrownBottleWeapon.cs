using UnityEngine;

public class ThrownBottleWeapon : MonoBehaviour
{
    [SerializeField] private GameObject damageAreaObject;
    [SerializeField] private float destroyTime = 1f;

    private void Update()
    {
        destroyTime -= Time.deltaTime;
        if (destroyTime <= 0f)
        {
            Instantiate(damageAreaObject, transform.position, Quaternion.identity).gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Instantiate(damageAreaObject, transform.position, Quaternion.identity).gameObject.SetActive(true);
            SFXManager.instance.PlaySoundEffectPitched(7);
            Destroy(gameObject);
        }
    }
}
