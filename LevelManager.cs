using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private float levelEndDelay = 2f;

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

        StartCoroutine(EndLevelRoutine());
    }

    IEnumerator EndLevelRoutine()
    {
        yield return new WaitForSeconds(levelEndDelay);

        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);

        UIController.instance.endTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        UIController.instance.endGamePanel.SetActive(true);
    }
}
