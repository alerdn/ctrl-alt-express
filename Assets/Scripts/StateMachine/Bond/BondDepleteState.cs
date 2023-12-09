using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BondDepleteState : BondBaseState
{
    private Coroutine _bondDepleteCoroutine;
    private ComboHandler _comboHandler;

    public BondDepleteState(BondStateMachine stateMachine, ComboHandler comboHandler) : base(stateMachine)
    {
        _comboHandler = comboHandler;
    }

    public override void Enter()
    {
    }

    public override void Tick(float deltaTime)
    {
        float distance = CalculateDistance();
        if (distance <= 5f)
        {
            stateMachine.SwitchState(new BondChargingState(stateMachine, _comboHandler));
            return;
        }

        if (_bondDepleteCoroutine == null)
        {
            _bondDepleteCoroutine = stateMachine.StartCoroutine((DepleteBond()));
        }
    }

    public override void Exit()
    {
        if (_bondDepleteCoroutine != null)
            stateMachine.StopCoroutine(_bondDepleteCoroutine);
    }

    private IEnumerator DepleteBond()
    {
        float distance = CalculateDistance();
        int depleteAmount = (int)Mathf.Ceil(distance / 5f);

        if (stateMachine.BondCharge.Value >= 0)
        {
            if (stateMachine.Bond.Value > 0)
            {
                stateMachine.Bond.Value -= depleteAmount;
                yield return new WaitForSeconds(.1f);
            }
            else if (stateMachine.BondCharge.Value > 0)
            {
                stateMachine.Bond.Value = 100;
                stateMachine.BondCharge.Value--;
            }
        }

        _bondDepleteCoroutine = null;
    }
}
