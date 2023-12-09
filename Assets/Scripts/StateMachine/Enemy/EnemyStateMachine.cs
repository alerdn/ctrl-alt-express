using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public AttackDamage[] Weapons { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public int StunChance { get; private set; } = 70;
    [field: SerializeField] public string FreeLookBlendTreeString = "FreeLookBlendTree";

    public CharacterStateMachine[] CharacterStateMachines { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }
    public Animator Animator { get; private set; }

    public void Init(CharacterStateMachine[] characterStateMachines)
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();

        CharacterStateMachines = characterStateMachines;
        SwitchState(new EnemyPersuingState(this, GetClosestCharacter()));
    }

    public CharacterStateMachine GetClosestCharacter()
    {
        var closestCharacter = CharacterStateMachines[0];
        var closestDistance = Vector3.Distance(transform.position, closestCharacter.transform.position);

        foreach (var character in CharacterStateMachines)
        {
            var distance = Vector3.Distance(transform.position, character.transform.position);

            if (distance < closestDistance)
            {
                closestCharacter = character;
                closestDistance = distance;
            }
        }

        return closestCharacter;
    }

    public float GetNormalizedTime(string tag)
    {
        AnimatorStateInfo currentInfo = Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = Animator.GetNextAnimatorStateInfo(0);

        if (Animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!Animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
}
