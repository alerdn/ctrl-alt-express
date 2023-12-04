using System.Collections;
using UnityEngine;

public class AbilityWind : AbilityBase
{
    [SerializeField] private ParticleSystem _windParticleSystem;

    public override void UseAbility(CharacterStateMachine stateMachine)
    {
        StartCoroutine(Ability(stateMachine));
    }

    private IEnumerator Ability(CharacterStateMachine stateMachine)
    {
        stateMachine.Character.Animator.CrossFadeInFixedTime(AnimationName, TransitionDuration);
        Instantiate(_windParticleSystem, transform.position, Quaternion.identity);

        yield return new WaitUntil(() => stateMachine.GetNormalizedTime("AbilityWind") >= 1f);

        OnAbilityFinish?.Invoke();
    }
}