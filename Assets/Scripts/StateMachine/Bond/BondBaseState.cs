using UnityEngine;

public abstract class BondBaseState : State
{
    protected BondStateMachine stateMachine;

    public BondBaseState(BondStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected float CalculateDistance()
    {
        return Vector3.Distance(stateMachine.Characters[0].transform.position, stateMachine.Characters[1].transform.position);
    }
}