using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamage : MonoBehaviour
{
    private string _myTag;
    private List<Collider> _alreadyCollidedWith;
    private int _damage;

    public void Init(string myTag, List<Collider> alreadyCollidedWith)
    {
        _myTag = myTag;
        _alreadyCollidedWith = alreadyCollidedWith;
    }

    public void SetAttack(int damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_myTag)) return;

        if (_alreadyCollidedWith.Contains(other)) return;
        _alreadyCollidedWith.Add(other);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player tomou dano");
        }
        else if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<Health>(out Health health))
            {
                Debug.Log($"Inimigo tomou {_damage} dano");
                health.DealDamage(_damage);
            }
        }
    }
}
