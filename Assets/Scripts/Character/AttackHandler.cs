using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [SerializeField] private string _myTag;
    [SerializeField] private StateMachine _stateMachine;
    [SerializeField] private AttackDamage[] _footHitBoxes;
    [SerializeField] private AttackDamage[] _handHitBoxes;

    private List<Collider> _alreadyCollidedWith = new List<Collider>();

    private void Start()
    {
        foreach (AttackDamage hitBox in _footHitBoxes)
        {
            hitBox.Init(_myTag, _alreadyCollidedWith, _stateMachine);
        }

        foreach (AttackDamage hitBox in _handHitBoxes)
        {
            hitBox.Init(_myTag, _alreadyCollidedWith, _stateMachine);
        }

        DisableFootHitBoxes();
        DisableHandHitBoxes();
    }

    public void EnableFootHitBoxes()
    {
        foreach (AttackDamage hitBox in _footHitBoxes)
        {
            hitBox.gameObject.SetActive(true);
        }

        _alreadyCollidedWith.Clear();
    }

    public void DisableFootHitBoxes()
    {
        foreach (AttackDamage hitBox in _footHitBoxes)
        {
            hitBox.gameObject.SetActive(false);
        }
    }

    public void EnableHandHitBoxes()
    {
        foreach (AttackDamage hitBox in _handHitBoxes)
        {
            hitBox.gameObject.SetActive(true);
        }

        _alreadyCollidedWith.Clear();
    }

    public void DisableHandHitBoxes()
    {
        foreach (AttackDamage hitBox in _handHitBoxes)
        {
            hitBox.gameObject.SetActive(false);
        }
    }
}
