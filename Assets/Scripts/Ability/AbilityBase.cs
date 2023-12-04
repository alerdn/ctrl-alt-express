using System;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    public Action OnAbilityFinish;

    [field: SerializeField] public string AnimationName { get; private set; }
    [field: SerializeField] public float TransitionDuration { get; private set; }

    public abstract void UseAbility(CharacterStateMachine stateMachine);
}