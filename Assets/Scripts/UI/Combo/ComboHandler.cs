using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ComboHandler : MonoBehaviour
{
    public int ChargeMultiplier => _rankIndex + 1;
    [SerializeField] private Sprite[] _ranks;
    [SerializeField] private Image _rankImage;
    [SerializeField] private TMP_Text _comboText;
    [SerializeField] private GameObject _comboPanel;
    [SerializeField] private RectTransform _comboProgress;

    private int _comboCount = 0;
    private int _rankIndex = 0;
    private Tween _comboTween;

    private void Start()
    {
        _comboCount = 0;
        _comboPanel.SetActive(false);
    }

    public void AddCombo(int damage)
    {
        CancelInvoke(nameof(ResetCombo));
        _comboPanel.SetActive(true);

        if (_comboCount == 0)
        {
            _rankImage.sprite = _ranks[0];
        }
        _comboCount += damage;
        _comboText.text = _comboCount.ToString();

        _rankIndex = (int)Mathf.Floor(_comboCount / 5000);
        _rankIndex = Mathf.Clamp(_rankIndex, 0, _ranks.Length - 1);
        _rankImage.sprite = _ranks[_rankIndex];

        if (_comboTween != null) _comboTween.Kill();
        _comboProgress.sizeDelta = new Vector2(_comboProgress.sizeDelta.x, 0f);
        _comboTween = DOTween.To(() => _comboProgress.sizeDelta, (size) => _comboProgress.sizeDelta = size, new Vector2(_comboProgress.sizeDelta.x, 105f), 9f);

        Invoke(nameof(ResetCombo), 8f);
    }

    private void ResetCombo()
    {
        _comboCount = 0;
        _rankIndex = 0;
        _comboPanel.SetActive(false);
    }
}
