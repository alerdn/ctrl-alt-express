using System;
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
        stateMachine.InputReader.OnSpecialAttackEvent += UseAbility;
        stateMachine.Character.Animator.CrossFadeInFixedTime(stateMachine.Character.FreeLookBlendTreeHash, 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        if (!stateMachine.IsCurrent)
        {
            stateMachine.SwitchState(new CharacterFollowState(stateMachine));
            return;
        }
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new CharacterAttackingState(stateMachine, 0));
            return;
        }

        Vector3 movement = stateMachine.CalculeMovement();
        Move(stateMachine.FreeLookMovement * movement, deltaTime);

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
        stateMachine.InputReader.OnSpecialAttackEvent -= UseAbility;
    }

    private void UseBondCharge()
    {
        if (stateMachine.BondStateMachine.BondCharge.Value <= 0)
        {
            return;
        }

        Collider[] colliders = Physics.OverlapSphere(stateMachine.transform.position, stateMachine.InteractionRange);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                interactable.Interact(stateMachine);
                stateMachine.BondStateMachine.BondCharge.Value--;
                break;
            }
        }
    }
}
