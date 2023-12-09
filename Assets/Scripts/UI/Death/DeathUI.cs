using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _kanji;
    [SerializeField] private AudioSource _deathSound;

    [Header("Settings")]
    [SerializeField] private float _fadeDuration = .5f;
    [SerializeField] private float _kanjiTime = 1f;
    [SerializeField] private float _resetLevelTime = 2f;

    private void Start()
    {
        _background.DOFade(0, 0);
        _kanji.enabled = false;
    }

    public void ShowDeathUI()
    {
        _background.DOFade(1, _fadeDuration);
        Invoke(nameof(ShowKanji), _kanjiTime);
    }

    private void ShowKanji()
    {
        _kanji.enabled = true;
        _deathSound.Play();

        Invoke(nameof(ResetLevel), _resetLevelTime);
    }

    private void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
