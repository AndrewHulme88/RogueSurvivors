using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField] private GameObject confirmButton;

    public static GameObject selectedCharacter;

    public GameObject[] characterPrefabs;
    public CharacterSelectCard[] characterCards;

    private void Start()
    {
        for (int i = 0; i < characterCards.Length; i++)
        {
            GameObject characterPrefab = characterPrefabs[i];
            PlayerController characterController = characterPrefab.GetComponent<PlayerController>();

            string characterName = characterController.characterName;
            Sprite characterSprite = characterController.characterSprite;
            characterCards[i].SetCharacterInfo(characterName, characterSprite);
        }
    }

    public void SelectCharacter(int index)
    {
        selectedCharacter = characterPrefabs[index];
        confirmButton.SetActive(true);
    }
}
