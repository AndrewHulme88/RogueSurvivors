using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Instantiate(CharacterSelector.selectedCharacter, Vector3.zero, Quaternion.identity);
    }
}
