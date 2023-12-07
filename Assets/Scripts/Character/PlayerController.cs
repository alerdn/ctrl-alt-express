using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterStateMachine[] CharacterStateMachines { get; private set; }
    [field: SerializeField] public BondStateMachine BondStateMachine { get; private set; }

    [SerializeField] private CinemachineTargetGroup _targetGroup;

    private int _currentCharacterIndex;
    private float _switchCharacterCooldown;

    private void Start()
    {
        InputReader.OnSwitchCharacterEvent += SwitchCharacter;
        SetCharacter(0);

        for (int i = 0; i < CharacterStateMachines.Length; i++)
        {
            CharacterStateMachine stateMachine = CharacterStateMachines[i];
            stateMachine.OnSwitchCharecter += SwitchCharacter;
            stateMachine.Init(InputReader, BondStateMachine, CharacterStateMachines[(i + 1) % 2]);
        }

        BondStateMachine.Init(new Character[] { CharacterStateMachines[0].Character, CharacterStateMachines[1].Character });
    }

    private void OnDestroy()
    {
        for (int i = 0; i < CharacterStateMachines.Length; i++)
        {
            CharacterStateMachine stateMachine = CharacterStateMachines[i];
            stateMachine.OnSwitchCharecter -= SwitchCharacter;
        }
    }

    private void Update()
    {
        if (_switchCharacterCooldown > 0)
        {
            _switchCharacterCooldown -= Time.deltaTime;
        }
    }

    private void SetCharacter(int characterIndex)
    {
        _currentCharacterIndex = characterIndex;
        int otherCharacterIndex = (_currentCharacterIndex + 1) % 2;

        CharacterStateMachines[_currentCharacterIndex].IsCurrent = true;
        CharacterStateMachines[otherCharacterIndex].IsCurrent = false;

        _targetGroup.m_Targets[_currentCharacterIndex].weight = 2f;
        _targetGroup.m_Targets[otherCharacterIndex].weight = 1f;
    }

    private void SwitchCharacter()
    {
        if (_switchCharacterCooldown > 0) return;

        int otherCharacterIndex = _currentCharacterIndex;
        _currentCharacterIndex = (_currentCharacterIndex + 1) % 2;

        CharacterStateMachines[_currentCharacterIndex].IsCurrent = true;
        CharacterStateMachines[otherCharacterIndex].IsCurrent = false;

        _targetGroup.m_Targets[_currentCharacterIndex].weight = 2f;
        _targetGroup.m_Targets[otherCharacterIndex].weight = 1f;

        _switchCharacterCooldown = .5f;
    }
}
