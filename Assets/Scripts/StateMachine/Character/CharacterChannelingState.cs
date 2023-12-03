using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChannelingState : CharacterBaseState
{
    public event Action OnChannelingCompleted;

    public readonly int StartChannelingHash = Animator.StringToHash("StartChanneling");
    public readonly int EndChannelingHash = Animator.StringToHash("EndChanneling");

    private Transform _lookAt;

    public CharacterChannelingState(CharacterStateMachine stateMachine, Transform lookAt) : base(stateMachine)
    {
        _lookAt = lookAt;
    }

    public override void Enter()
    {
        stateMachine.SwitchCharacter();
        stateMachine.StartCoroutine(ChannelingRoutine());
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void Exit()
    {
    }

    private IEnumerator ChannelingRoutine()
    {
        Vector3 lookAtPosition = _lookAt.position;
        stateMachine.transform.LookAt(new Vector3(lookAtPosition.x, 0f, lookAtPosition.z));

        stateMachine.Character.Animator.CrossFadeInFixedTime(StartChannelingHash, 0.1f);
        yield return new WaitForSeconds(stateMachine.ChannelingTime);

        stateMachine.Character.Animator.CrossFadeInFixedTime(EndChannelingHash, 0.1f);
        yield return new WaitForSeconds(1.5f);

        stateMachine.SwitchState(new CharacterFollowState(stateMachine));

        OnChannelingCompleted?.Invoke();
    }
}
