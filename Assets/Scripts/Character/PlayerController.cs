using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using NaughtyAttributes;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterStateMachine[] CharacterStateMachines { get; private set; }
    [field: SerializeField] public BondStateMachine BondStateMachine { get; private set; }
    [field: SerializeField] public CharacterHandler CharacterHandler { get; private set; }

    [SerializeField] private CinemachineTargetGroup _targetGroup;
    [SerializeField] private DeathUI _deathUI;

    private int _currentCharacterIndex;
    private float _switchCharacterCooldown;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        InputReader.OnSwitchCharacterEvent += SwitchCharacter;
        SetCharacter(0);

        for (int i = 0; i < CharacterStateMachines.Length; i++)
        {
            CharacterStateMachine stateMachine = CharacterStateMachines[i];
            stateMachine.OnSwitchCharecter += SwitchCharacter;
            stateMachine.Init(InputReader, BondStateMachine, CharacterStateMachines[(i + 1) % 2]);
        }

        BondStateMachine.Init(new Character[] { CharacterStateMachines[0].Character, CharacterStateMachines[1].Character }, CharacterStateMachines[0].ComboHandler);
        BondStateMachine.OnPlayerDeath += OnplayerDeath;
    }

    private void OnDestroy()
    {
        for (int i = 0; i < CharacterStateMachines.Length; i++)
        {
            CharacterStateMachine stateMachine = CharacterStateMachines[i];
            stateMachine.OnSwitchCharecter -= SwitchCharacter;
        }
        BondStateMachine.OnPlayerDeath -= OnplayerDeath;
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

        CharacterHandler.SetCharacter(_currentCharacterIndex);
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

        CharacterHandler.SetCharacter(_currentCharacterIndex);
    }

    private void OnplayerDeath()
    {
        CharacterStateMachines[0].SwitchState(new CharacterDeathState(CharacterStateMachines[0]));
        CharacterStateMachines[1].SwitchState(new CharacterDeathState(CharacterStateMachines[1]));

        _deathUI.ShowDeathUI();
    }
}
