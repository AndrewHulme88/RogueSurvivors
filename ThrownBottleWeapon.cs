using UnityEngine;

public class ThrownBottleWeapon : MonoBehaviour
{
    [SerializeField] private GameObject damageAreaObject;

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
