using System;
using UnityEngine;

public class CharacterAttackingState : CharacterBaseState
{
    private Attack _attack;
    private bool _alreadyAppliedForce;
    private bool _autoAttack;
    private Transform _enemy;

    public CharacterAttackingState(CharacterStateMachine stateMachine, int attackIndex, bool autoAttack = false, Transform enemy = null) : base(stateMachine)
    {
        _attack = stateMachine.Attacks[attackIndex];
        _autoAttack = autoAttack;
        _enemy = enemy;
    }

    public override void Enter()
    {
        foreach (AttackDamage weapon in stateMachine.Weapons)
        {
            weapon.SetAttack(_attack.Damage);
        }
        if (!_autoAttack) stateMachine.InputReader.OnSpecialAttackEvent += UseAbility;
        stateMachine.Character.Animator.CrossFadeInFixedTime(_attack.AnimationName, _attack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (!_autoAttack) Move(deltaTime);

        float normalizedTime = stateMachine.GetNormalizedTime("Attack");

        if (normalizedTime >= _attack.ForceTime)
        {
            TryApplyForce();
        }

        if (normalizedTime < 1f)
        {
            if (stateMachine.InputReader.IsAttacking || _autoAttack)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            if (stateMachine.IsCurrent)
                stateMachine.SwitchState(new CharacterFreeLookState(stateMachine));
            else stateMachine.SwitchState(new CharacterFollowState(stateMachine));
        }

        if (_autoAttack && _enemy != null)
        {
            Vector3 lookAtPosition = _enemy.position - stateMachine.transform.position;
            stateMachine.transform.rotation = Quaternion.LookRotation(new Vector3(lookAtPosition.x, 0f, lookAtPosition.z));
        }
    }

    public override void Exit()
    {
        if (!_autoAttack) stateMachine.InputReader.OnSpecialAttackEvent -= UseAbility;
    }

    private void TryComboAttack(float normalizedTime)
    {
        if (_attack.ComboStateIndex == -1) return;

        if (normalizedTime < _attack.ComboAttackTime) return;

        stateMachine.SwitchState(new CharacterAttackingState(stateMachine, _attack.ComboStateIndex, _autoAttack, _enemy));
    }

    private void TryApplyForce()
    {
        if (_alreadyAppliedForce) return;

        Vector3 movement = stateMachine.CalculeMovement();
        if (movement != Vector3.zero && stateMachine.IsCurrent)
        {
            stateMachine.Character.transform.rotation = Quaternion.LookRotation(movement);
            stateMachine.ForceReceiver.AddForce((stateMachine.transform.forward + movement) * _attack.Force);
        }
        _alreadyAppliedForce = true;
    }
}