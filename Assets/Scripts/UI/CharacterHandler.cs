using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] _characterImages;

    public void SetCharacter(int characterIndex)
    {
        int otherCharacterIndex = (characterIndex + 1) % 2;

        _characterImages[characterIndex].SetActive(false);
        _characterImages[otherCharacterIndex].SetActive(true);
    }
}
