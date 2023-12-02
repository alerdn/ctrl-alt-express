using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public Character[] Characters { get; private set; }
    [field: SerializeField] public float FreeLookMovement { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }

    public Character CurrentCharacter { get; private set; }
    public Transform MainCameraTrasform { get; private set; }

    private void Start()
    {
        CurrentCharacter = Characters[0];
        MainCameraTrasform = Camera.main.transform;

        InputReader.OnChangeToKunoichiEvent += ChangeToKunoichi;
        InputReader.OnChangeToNinjaEvent += ChangeToNinja;

        SwitchState(new PlayerFreeLookState(this));
    }

    private void ChangeToKunoichi()
    {
        CurrentCharacter = Characters[0];
    }

    private void ChangeToNinja()
    {
        CurrentCharacter = Characters[1];
    }
}
