using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;

    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void DealDamage(int damage)
    {
        if (_currentHealth <= 0) return;

        _currentHealth = Mathf.Max(_currentHealth - damage, 0);
    }
}
