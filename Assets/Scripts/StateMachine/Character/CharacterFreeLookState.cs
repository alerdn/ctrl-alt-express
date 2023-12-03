using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFreeLookState : CharacterBaseState
{
    public CharacterFreeLookState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.InputReader.OnActionEvent += UseBondCharge;
    }

    public override void Tick(float deltaTime)
    {
        if (!stateMachine.IsCurrent)
        {
            stateMachine.SwitchState(new CharacterFollowState(stateMachine));
            return;
        }

        Vector3 movement = CalculeMovement();

        stateMachine.Character.Controller.Move(stateMachine.FreeLookMovement * deltaTime * movement);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Character.Animator.SetFloat(FreeLookSpeedHash, 0f, AnimatorDampTime, deltaTime);
            return;
        }

        stateMachine.Character.Animator.SetFloat(FreeLookSpeedHash, 1f, AnimatorDampTime, deltaTime);
        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.OnActionEvent -= UseBondCharge;
    }

    private Vector3 CalculeMovement()
    {
        Vector3 forward = stateMachine.MainCamera.transform.forward;
        Vector3 right = stateMachine.MainCamera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y
            + right * stateMachine.InputReader.MovementValue.x;
    }

    private void UseBondCharge()
    {
        if (stateMachine.BondStateMachine.BondCharge.Value <= 0)
        {
            return;
        }

        stateMachine.BondStateMachine.BondCharge.Value--;
    }
}
