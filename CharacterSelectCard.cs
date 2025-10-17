using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectCard : MonoBehaviour
{
    [SerializeField] private TMP_Text characterNameText;
    [SerializeField] private Image characterImage;

    public void SetCharacterInfo(string name, Sprite image)
    {
        characterNameText.text = name;
        characterImage.sprite = image;
    }
}
