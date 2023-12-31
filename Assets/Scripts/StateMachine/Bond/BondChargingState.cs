using System;
using System.Collections;
using UnityEngine;

public class BondChargingState : BondBaseState
{
    private Coroutine _bondChargingCoroutine;
    private ComboHandler _comboHandler;

    public BondChargingState(BondStateMachine stateMachine, ComboHandler comboHandler) : base(stateMachine)
    {
        _comboHandler = comboHandler;
    }

    public override void Enter()
    {
        stateMachine.OnBondDamaged += OnBondDamaged;
    }

    public override void Tick(float deltaTime)
    {
        float distance = CalculateDistance();
        if (distance > 8f)
        {
            stateMachine.SwitchState(new BondDepleteState(stateMachine, _comboHandler));
            return;
        }

        if (_bondChargingCoroutine == null)
        {
            _bondChargingCoroutine = stateMachine.StartCoroutine((ChargeBond()));
        }
    }

    public override void Exit()
    {
        if (_bondChargingCoroutine != null)
            stateMachine.StopCoroutine(_bondChargingCoroutine);
    }

    private IEnumerator ChargeBond()
    {
        if (stateMachine.Bond.Value < 100)
        {
            stateMachine.Bond.Value += _comboHandler.ChargeMultiplier;
            yield return new WaitForSeconds(.1f);
        }
        else
        {
            if (stateMachine.BondCharge.Value < stateMachine.MaxBondCharge)
            {
                stateMachine.BondCharge.Value++;
                stateMachine.Bond.Value = 0;
            }
        }

        _bondChargingCoroutine = null;
    }

    private void OnBondDamaged()
    {
        if (_bondChargingCoroutine != null)
            stateMachine.StopCoroutine(_bondChargingCoroutine);

        _bondChargingCoroutine = stateMachine.StartCoroutine(PauseBondRoutine());
    }

    private IEnumerator PauseBondRoutine()
    {
        yield return new WaitForSeconds(1f);
        _bondChargingCoroutine = stateMachine.StartCoroutine(ChargeBond());
    }
}
