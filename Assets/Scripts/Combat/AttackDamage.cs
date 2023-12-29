using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamage : MonoBehaviour
{
    private string _myTag;
    private List<Collider> _alreadyCollidedWith;
    private StateMachine _stateMachine;
    private int _damage;

    public void Init(string myTag, List<Collider> alreadyCollidedWith, StateMachine stateMachine)
    {
        _myTag = myTag;
        _alreadyCollidedWith = alreadyCollidedWith;
        _stateMachine = stateMachine;
    }

    public void SetAttack(int damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_myTag == null) return;

        if (other.CompareTag(_myTag)) return;

        if (_alreadyCollidedWith.Contains(other)) return;
        _alreadyCollidedWith.Add(other);

        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<CharacterStateMachine>(out CharacterStateMachine characterStateMachine))
            {
                characterStateMachine.BondStateMachine.DamageBond(_damage);
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<Health>(out Health health))
            {
                CharacterStateMachine characterStateMachine = ((CharacterStateMachine)_stateMachine);
                characterStateMachine.ComboHandler.AddCombo(_damage);
                characterStateMachine.PlayAudio(characterStateMachine.AttackAudios.GetRandom());
                health.DealDamage(_damage);
            }
        }
    }
}
