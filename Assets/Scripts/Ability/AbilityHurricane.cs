using System;
using System.Collections;
using UnityEngine;

public class AbilityHurricane : AbilityBase
{
    [field: SerializeField] public float AnimationDuration { get; private set; }
    [SerializeField] private float _force = 1f;
    [SerializeField] private int _damage = 100;

    public override void UseAbility(CharacterStateMachine stateMachine)
    {
        StartCoroutine(Ability(stateMachine));
    }

    private IEnumerator Ability(CharacterStateMachine stateMachine)
    {
        foreach (AttackDamage weapon in stateMachine.Weapons)
        {
            weapon.SetAttack(_damage);
        }
        stateMachine.Character.Animator.CrossFadeInFixedTime(AnimationName, TransitionDuration);
        float duration = AnimationDuration;

        while (duration > 0f)
        {
            duration -= Time.deltaTime;
            Vector3 movement = Vector3.zero;
            
            if (stateMachine.IsCurrent) movement = stateMachine.CalculeMovement();
            if (movement == Vector3.zero) movement = stateMachine.transform.forward;

            stateMachine.Character.Controller.Move(movement * _force * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        OnAbilityFinish?.Invoke();
    }
}