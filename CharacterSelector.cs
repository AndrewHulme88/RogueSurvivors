using JetBrains.Annotations;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public static GameObject selectedCharacter;

    public GameObject[] characterPrefabs;

    public void SelectCharacter(int index)
    {
        selectedCharacter = characterPrefabs[index];
    }
}
