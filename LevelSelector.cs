using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private GameObject confirmButton;
    
    private string selectedLevelName = null;

    public void SelectLevel(string levelToSelect)
    {
        selectedLevelName = levelToSelect;
        confirmButton.SetActive(true);
    }

    public void ConfirmLevel()
    {
        if (!string.IsNullOrEmpty(selectedLevelName))
        {
            SceneManager.LoadScene(selectedLevelName);        
        }
    }
}
