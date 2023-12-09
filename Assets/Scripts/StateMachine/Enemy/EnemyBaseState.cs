using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    protected const float AnimatorDampTime = .075f;

    protected EnemyStateMachine stateMachine;
    protected int FreeLookBlendTreeHash;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        FreeLookBlendTreeHash = Animator.StringToHash(stateMachine.FreeLookBlendTreeString);
    }

    protected void OnDamage(int health)
    {
        // Ao sofrer dano, inimigo é interrompido
        if (Random.Range(0, 100) < stateMachine.StunChance)
        {
            stateMachine.SwitchState(new EnemyStunnedState(stateMachine));
        }
    }
}
