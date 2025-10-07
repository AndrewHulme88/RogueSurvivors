using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Transform sprite;
    [SerializeField] private float animationSpeed = 5f;
    [SerializeField] private float minSize = 0.8f;
    [SerializeField] private float maxSize = 1f;

    private float currentSize;

    private void Start()
    {
        currentSize = maxSize;
    }

    private void Update()
    {
        currentSize = Mathf.PingPong(Time.time * animationSpeed, maxSize - minSize) + minSize;
        sprite.localScale = new Vector3(currentSize, currentSize, 1f);
    }
}
