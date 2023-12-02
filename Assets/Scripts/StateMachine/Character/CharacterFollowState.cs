using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFollowState : CharacterBaseState
{
    public CharacterFollowState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Character.Animator.SetFloat(FreeLookSpeedHash, 0f);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.IsCurrent)
        {
            stateMachine.SwitchState(new CharacterFreeLookState(stateMachine));
        }
    }

    public override void Exit()
    {
    }
}
