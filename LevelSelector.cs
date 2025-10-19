using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private GameObject confirmButton;
    [SerializeField] private string levelName;

    public void SelectLevel()
    {
        //confirmButton.SetActive(true);
        SceneManager.LoadScene(levelName);
    }

    //public void ConfirmLevel()
    //{
    //    SceneManager.LoadScene(levelName);
    //}
}
