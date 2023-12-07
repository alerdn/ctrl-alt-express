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

    [field: SerializeField] public ComboHandler ComboHandler { get; private set; }
    [field: SerializeField] public Character Character { get; private set; }
    [field: SerializeField] public float FreeLookMovement { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public float InteractionRange { get; private set; } = 2f;
    [field: SerializeField] public AttackDamage[] Weapons { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }
    [field: SerializeField] public AbilityBase[] Abilities { get; private set; }

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

    public float GetNormalizedTime(string tag)
    {
        AnimatorStateInfo currentInfo = Character.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = Character.Animator.GetNextAnimatorStateInfo(0);

        if (Character.Animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!Character.Animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }

    public Vector3 CalculeMovement()
    {
        Vector3 forward = MainCamera.transform.forward;
        Vector3 right = MainCamera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * InputReader.MovementValue.y
            + right * InputReader.MovementValue.x;
    }
}
