using UnityEngine;

public class CoinController : MonoBehaviour
{
    public static CoinController instance;

    [SerializeField] private PickupCoin coinPrefab;
    [SerializeField] private float coinSpawnOffsetX = 0.2f;
    [SerializeField] private float coinSpawnOffsetY = 0.1f;

    public int currentCoins = 0;

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

    public void AddCoins(int amount)
    {
        currentCoins += amount;
        UIController.instance.UpdateCoins(currentCoins);
        SFXManager.instance.PlaySoundEffectPitched(2);
    }

    public void DropCoin(Vector3 position, int value)
    {
        PickupCoin newCoin = Instantiate(coinPrefab, position + new Vector3(coinSpawnOffsetX, coinSpawnOffsetY, 0f), Quaternion.identity);
        newCoin.coinAmount = value;
        newCoin.gameObject.SetActive(true);
    }

    public void SpendCoins(int amount)
    {
        if (currentCoins >= amount)
        {
            currentCoins -= amount;
            UIController.instance.UpdateCoins(currentCoins);
        }
    }
}
