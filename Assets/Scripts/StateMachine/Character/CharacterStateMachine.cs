using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterStateMachine : StateMachine
{
    public InputReader InputReader { get; private set; }
    public BondStateMachine BondStateMachine { get; private set; }
    public CharacterStateMachine OtherCharacter { get; private set; }

    [field: SerializeField] public Character Character { get; private set; }
    [field: SerializeField] public float FreeLookMovement { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }

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
}
