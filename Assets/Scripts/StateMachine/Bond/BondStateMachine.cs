using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BondStateMachine : StateMachine
{
    public Character[] Characters { get; private set; }
    [field: SerializeField] public SOInt Bond { get; private set; }
    [field: SerializeField] public SOInt BondCharge { get; private set; }
    [field: SerializeField] public int MaxBondCharge { get; private set; }

    [SerializeField] private LineRenderer _bondRenderer;

    private void Start()
    {
        Bond.Value = 0;
        BondCharge.Value = 0;
    }

    protected override void Update()
    {
        base.Update();
        DrawBond();
    }

    public void Init(Character[] characters)
    {
        Characters = characters;
        SwitchState(new BondChargingState(this));
    }

    private void DrawBond()
    {
        _bondRenderer.SetPosition(0, new Vector3(Characters[0].transform.position.x, .5f, Characters[0].transform.position.z));
        _bondRenderer.SetPosition(1, new Vector3(Characters[1].transform.position.x, .5f, Characters[1].transform.position.z));
    }
}
