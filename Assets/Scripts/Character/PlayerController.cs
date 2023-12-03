using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterStateMachine[] CharacterStateMachines { get; private set; }
    [field: SerializeField] public BondStateMachine BondStateMachine { get; private set; }

    private int _currentCharacterIndex;

    private void Start()
    {
        InputReader.OnSwitchCharacterEvent += SwitchCharacter;
        SetCharacter(0);

        for (int i = 0; i < CharacterStateMachines.Length; i++)
        {
            CharacterStateMachines[i].Init(InputReader, BondStateMachine, CharacterStateMachines[(i + 1) % 2]);
        }

        BondStateMachine.Init(new Character[] { CharacterStateMachines[0].Character, CharacterStateMachines[1].Character });
    }

    private void SetCharacter(int characterIndex)
    {
        _currentCharacterIndex = characterIndex;
        CharacterStateMachines[_currentCharacterIndex].IsCurrent = true;
        CharacterStateMachines[(_currentCharacterIndex + 1) % 2].IsCurrent = false;
    }

    private void SwitchCharacter()
    {
        _currentCharacterIndex = (_currentCharacterIndex + 1) % 2;

        CharacterStateMachines[_currentCharacterIndex].IsCurrent = true;
        CharacterStateMachines[(_currentCharacterIndex + 1) % 2].IsCurrent = false;
    }
}
