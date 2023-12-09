using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<int> OnDamage;
    public event Action OnDeath;
    public int MaxHealth => _maxHealth;

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
        OnDamage?.Invoke(_currentHealth);

        if (_currentHealth == 0)
        {
            OnDeath?.Invoke();
            Kill();
        }
    }

    private void Kill()
    {
        Instantiate(_onKillParticlesPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
