using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField] private EnemyStateMachine _bossStateMachine;
    [SerializeField] private GameObject _bossHealthUI;
    [SerializeField] private Image _healthBar;
    [SerializeField] private DialogueData[] _dialogue;
    [SerializeField] private DialogueData[] _finalDialogue;
    [SerializeField] private GameObject _devScreen;

    CharacterStateMachine[] _characterStateMachines;
    private int _zoneCount;

    public void Init(CharacterStateMachine[] characterStateMachines)
    {
        _characterStateMachines = characterStateMachines;
    }

    [Button]
    public void OnZoneCompleted()
    {
        _zoneCount++;
        if (_zoneCount == 5)
        {
            DialogueManager.Instance.StartDialogue(_dialogue);
            Invoke(nameof(InitBoss), 5f);
        }
    }

    private void InitBoss()
    {
        _bossStateMachine.gameObject.SetActive(true);
        _bossStateMachine.Init(_characterStateMachines);

        _bossHealthUI.SetActive(true);
        _bossStateMachine.Health.OnDamage += UpdateHealthBar;
        _bossStateMachine.Health.OnDeath += OnBossDeath;
    }

    private void UpdateHealthBar(int health)
    {
        _healthBar.fillAmount = (float)health / (float)_bossStateMachine.Health.MaxHealth;
    }

    private void OnBossDeath()
    {
        DialogueManager.Instance.StartDialogue(_finalDialogue);
        Invoke(nameof(EndGame), 12f);
    }

    private void EndGame()
    {
        _devScreen.SetActive(true);
        Invoke(nameof(QuitGame), 5f);
    }

    private void QuitGame()
    {
        SceneManager.LoadScene(0);
    }
}
