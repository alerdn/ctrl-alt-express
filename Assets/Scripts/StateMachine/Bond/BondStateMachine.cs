using System;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;

public class BondStateMachine : StateMachine
{
    public event Action OnBondDamaged;
    public event Action OnPlayerDeath;

    public Character[] Characters { get; private set; }
    [field: SerializeField] public SOInt Bond { get; private set; }
    [field: SerializeField] public SOInt BondCharge { get; private set; }
    [field: SerializeField] public int MaxBondCharge { get; private set; }
    public bool Intangible;

    [SerializeField] private LineRenderer _bondRenderer;
    [SerializeField] private Volume _damageVolume;

    private Tween _damageTween;
    private ComboHandler _comboHandler;
    private bool _gameOver;

    private void Start()
    {
        Bond.Value = 100;
        BondCharge.Value = 3;
    }

    protected override void Update()
    {
        base.Update();
        DrawBond();
    }

    public void Init(Character[] characters, ComboHandler comboHandler)
    {
        Characters = characters;
        _comboHandler = comboHandler;
        SwitchState(new BondChargingState(this, _comboHandler));
    }

    private void DrawBond()
    {
        _bondRenderer.SetPosition(0, new Vector3(Characters[0].transform.position.x, Characters[0].transform.position.y + 1f, Characters[0].transform.position.z));
        _bondRenderer.SetPosition(1, new Vector3(Characters[1].transform.position.x, Characters[0].transform.position.y + 1f, Characters[1].transform.position.z));
    }

    public void DamageBond(int damage)
    {
        if (_gameOver) return;
        if (Intangible) return;

        OnBondDamaged?.Invoke();
        int remainingDamage = 0;
        if (Bond.Value - damage < 0)
        {
            remainingDamage = damage - Bond.Value;
        }
        Bond.Value -= damage;

        UpdateDamageVolume();

        // Acabou a barra, mas tem charge
        if (Bond.Value <= 0 && BondCharge.Value > 0)
        {
            Bond.Value = 100 - remainingDamage;
            BondCharge.Value--;
        }
        // Acabou a barra e não tem charge
        else if (Bond.Value <= 0 && BondCharge.Value <= 0)
        {
            _gameOver = true;
            OnPlayerDeath?.Invoke();
        }
    }

    private void UpdateDamageVolume()
    {
        _damageVolume.weight = (1f - ((Bond.Value + 1) * BondCharge.Value / 100f)) / 2f;

        if (_damageTween != null) _damageTween.Kill();
        _damageTween = DOTween.To(() => _damageVolume.weight, x => _damageVolume.weight = x, 1f, .25f).SetLoops(2, LoopType.Yoyo);
    }
}
