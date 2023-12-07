using System;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;

public class BondStateMachine : StateMachine
{
    public event Action OnBondDamaged;

    public Character[] Characters { get; private set; }
    [field: SerializeField] public SOInt Bond { get; private set; }
    [field: SerializeField] public SOInt BondCharge { get; private set; }
    [field: SerializeField] public int MaxBondCharge { get; private set; }
    public bool Intangible;

    [SerializeField] private LineRenderer _bondRenderer;

    private Tween _damageTween;
    private ComboHandler _comboHandler;

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
        _bondRenderer.SetPosition(0, new Vector3(Characters[0].transform.position.x, .5f, Characters[0].transform.position.z));
        _bondRenderer.SetPosition(1, new Vector3(Characters[1].transform.position.x, .5f, Characters[1].transform.position.z));
    }

    public void DamageBond(int damage)
    {
        if (Intangible) return;

        OnBondDamaged?.Invoke();
        Bond.Value -= damage;

        // Acabou a barra, mas tem charge
        if (Bond.Value <= 0 && BondCharge.Value > 0)
        {
            Bond.Value = 100;
            BondCharge.Value--;
        }
        // Acabou a barra e não tem charge
        else if (Bond.Value <= 0 && BondCharge.Value <= 0)
        {
            // Game Over
            Debug.Log("Game Over");
        }
    }
}
