using System;
using UnityEngine;

public class CharacterAttackingState : CharacterBaseState
{
    private Attack _attack;
    private float _previousFrameTime;
    private bool _alreadyAppliedForce;

    public CharacterAttackingState(CharacterStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        _attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.Character.Animator.CrossFadeInFixedTime(_attack.AnimationName, _attack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        float normalizedTime = GetNormalizedTime();

        if (normalizedTime >= _attack.ForceTime)
        {
            TryApplyForce();
        }

        if (normalizedTime < 1f)
        {
            if (stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            stateMachine.SwitchState(new CharacterFreeLookState(stateMachine));
        }

        _previousFrameTime = normalizedTime;
    }

    public override void Exit()
    {
    }

    private void TryComboAttack(float normalizedTime)
    {
        if (_attack.ComboStateIndex == -1) return;

        if (normalizedTime < _attack.ComboAttackTime) return;

        stateMachine.SwitchState(new CharacterAttackingState(stateMachine, _attack.ComboStateIndex));

    }

    private void TryApplyForce()
    {
        if (_alreadyAppliedForce) return;

        Vector3 movement = CalculeMovement();
        if (movement != Vector3.zero)
            stateMachine.Character.transform.rotation = Quaternion.LookRotation(movement);

        stateMachine.ForceReceiver.AddForce((stateMachine.transform.forward + movement) * _attack.Force);
        _alreadyAppliedForce = true;
    }

    private float GetNormalizedTime()
    {
        AnimatorStateInfo currentInfo = stateMachine.Character.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = stateMachine.Character.Animator.GetNextAnimatorStateInfo(0);

        if (stateMachine.Character.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if (!stateMachine.Character.Animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
}