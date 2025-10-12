using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float timer = 0f;

    private bool isGameActive;

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

    private void Start()
    {
        isGameActive = true;
    }

    void Update()
    {
        if (isGameActive)
        {
            timer += Time.deltaTime;
            UIController.instance.UpdateTimer(timer);
        }
    }

    public void EndGame()
    {
        isGameActive = false;
    }
}
