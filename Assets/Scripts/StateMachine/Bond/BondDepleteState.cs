using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BondDepleteState : BondBaseState
{
    private Coroutine _bondDepleteCoroutine;

    public BondDepleteState(BondStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
    }

    public override void Tick(float deltaTime)
    {
        float distance = CalculateDistance();
        if (distance <= 8f)
        {
            stateMachine.SwitchState(new BondChargingState(stateMachine));
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
        if (stateMachine.BondCharge.Value >= 0)
        {
            if (stateMachine.Bond.Value > 0)
            {
                stateMachine.Bond.Value--;
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
