using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    public CharacterStateMachine[] CharacterStateMachines { get; private set; }

    [SerializeField] private Transform _spawnParent;
    [SerializeField] private AnimationCurve _spawnCurve;
    [SerializeField] private AnimationCurve _spawnIntervalCurve;
    [SerializeField] private EnemyStateMachine[] _enemyPrefab;
    [SerializeField] private WhiteFlameInterectable _whiteFlameInterectable;

    private List<Transform> _spawnPoints = new();

    private bool _alreadySpawned;
    private int _waveIndex;

    private void Start()
    {
        _whiteFlameInterectable.OnFlamedLited += OnFlamedLited;

        foreach (Transform child in _spawnParent)
        {
            _spawnPoints.Add(child);
        }
    }

    public void Init(CharacterStateMachine[] characterStateMachines)
    {
        CharacterStateMachines = characterStateMachines;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_alreadySpawned) return;
        _alreadySpawned = true;

        if (other.CompareTag("Player"))
        {
            Invoke(nameof(SpawnWave), _spawnIntervalCurve.Evaluate(_waveIndex));
        }
    }

    private void OnFlamedLited()
    {
        CancelInvoke(nameof(SpawnWave));
    }

    private void SpawnWave()
    {
        int spawnAmount = Mathf.RoundToInt(_spawnCurve.Evaluate(_waveIndex));

        for (int i = 0; i < spawnAmount; i++)
        {
            var enemy = Instantiate(_enemyPrefab.GetRandom(), _spawnPoints.GetRandom().position, Quaternion.identity);
            enemy.Init(CharacterStateMachines);
        }

        _waveIndex++;
        Invoke(nameof(SpawnWave), _spawnIntervalCurve.Evaluate(_waveIndex));
    }
}
