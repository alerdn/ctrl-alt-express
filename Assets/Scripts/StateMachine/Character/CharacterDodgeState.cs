using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDodgeState : CharacterBaseState
{
    private Vector3 _movementDirection;

    public CharacterDodgeState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Character.Animator.CrossFadeInFixedTime("Roll", .1f);
        stateMachine.BondStateMachine.Intangible = true;
        stateMachine.StartCoroutine(FinishDodge());
        _movementDirection = stateMachine.CalculeMovement();
    }

    public override void Tick(float deltaTime)
    {
        
        stateMachine.Move(stateMachine.transform.forward * 10 + _movementDirection, deltaTime);
    }

    public override void Exit()
    {

    }

    private IEnumerator FinishDodge()
    {
        yield return new WaitForSeconds(.5f);
        stateMachine.BondStateMachine.Intangible = false;
        stateMachine.SwitchState(new CharacterFreeLookState(stateMachine));
    }
}
