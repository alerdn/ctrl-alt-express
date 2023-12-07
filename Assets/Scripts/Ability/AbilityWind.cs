using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;

public class AbilityWind : AbilityBase
{
    [SerializeField] private ParticleSystem _windParticleSystem;
    [SerializeField] private Volume _abilitVolume;

    public override void UseAbility(CharacterStateMachine stateMachine)
    {
        StartCoroutine(Ability(stateMachine));
    }

    private IEnumerator Ability(CharacterStateMachine stateMachine)
    {
        stateMachine.Character.Animator.CrossFadeInFixedTime(AnimationName, TransitionDuration);
        Instantiate(_windParticleSystem, transform.position, Quaternion.identity);

        stateMachine.StartCoroutine(IntangibleRoutine(stateMachine));
        yield return new WaitUntil(() => stateMachine.GetNormalizedTime(AnimationName) >= 1f);

        OnAbilityFinish?.Invoke();
    }

    private IEnumerator IntangibleRoutine(CharacterStateMachine stateMachine)
    {
        SetIntangible(stateMachine);
        SetIntangible(stateMachine.OtherCharacter);

        yield return new WaitForSeconds(5f);

        SetTangible(stateMachine);
        SetTangible(stateMachine.OtherCharacter);
    }

    private void SetIntangible(CharacterStateMachine stateMachine)
    {
        stateMachine.BondStateMachine.Intangible = true;
        DOTween.To(() => _abilitVolume.weight, (weight) => _abilitVolume.weight = weight, 1f, .5f);
    }

    private void SetTangible(CharacterStateMachine stateMachine)
    {
        DOTween.To(() => _abilitVolume.weight, (weight) => _abilitVolume.weight = weight, 0f, .5f)
            .OnComplete(() => stateMachine.BondStateMachine.Intangible = false);
    }
}