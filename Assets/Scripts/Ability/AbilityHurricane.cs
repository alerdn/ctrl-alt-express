using System;
using System.Collections;
using UnityEngine;

public class AbilityHurricane : AbilityBase
{
    [field: SerializeField] public float AnimationDuration { get; private set; }
    [SerializeField] private float _force = 1f;

    public override void UseAbility(CharacterStateMachine stateMachine)
    {
        StartCoroutine(Ability(stateMachine));
    }

    private IEnumerator Ability(CharacterStateMachine stateMachine)
    {
        stateMachine.Character.Animator.CrossFadeInFixedTime(AnimationName, TransitionDuration);
        float duration = AnimationDuration;

        while (duration > 0f)
        {
            duration -= Time.deltaTime;
            stateMachine.Character.Controller.Move(stateMachine.transform.forward * _force * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        OnAbilityFinish?.Invoke();
    }
}