using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public Character Character { get; private set; }
    [field: SerializeField] public float FreeLookMovement { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }

    public bool IsCurrent { get; set; }
    public Camera MainCamera { get; private set; }

    private void Start()
    {
        MainCamera = Camera.main;
        SwitchState(new CharacterFreeLookState(this));
    }
}
