using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine : StateMachine
{
    public event Action OnSwitchCharecter;

    public InputReader InputReader { get; private set; }
    public BondStateMachine BondStateMachine { get; private set; }
    public CharacterStateMachine OtherCharacter { get; private set; }
    public float ChannelingTime = 5f;

    [field: SerializeField] public Character Character { get; private set; }
    [field: SerializeField] public float FreeLookMovement { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public float InteractionRange { get; private set; } = 2f;
    [field: SerializeField] public Attack[] Attacks { get; private set; }

    public bool IsCurrent { get; set; }
    public Camera MainCamera { get; private set; }

    private void Start()
    {
        MainCamera = Camera.main;
    }

    public void Init(InputReader inputReader, BondStateMachine bondStateMachine, CharacterStateMachine otherCharacter)
    {
        InputReader = inputReader;
        BondStateMachine = bondStateMachine;
        OtherCharacter = otherCharacter;

        SwitchState(new CharacterFreeLookState(this));
    }

    public void SwitchCharacter()
    {
        OnSwitchCharecter?.Invoke();
    }
}
