using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnDamage;

    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private ParticleSystem _onKillParticlesPrefab;

    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void DealDamage(int damage)
    {
        if (_currentHealth <= 0) return;

        _currentHealth = Mathf.Max(_currentHealth - damage, 0);
        OnDamage?.Invoke();

        if (_currentHealth == 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        Instantiate(_onKillParticlesPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
