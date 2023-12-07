using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected readonly int Attack1Hash = Animator.StringToHash("EnemyAttack1");
    protected readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    protected readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    protected const float AnimatorDampTime = .075f;

    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void OnDamage()
    {
        // Ao sofrer dano, inimigo é interrompido
        if (Random.Range(0, 100) < stateMachine.StunChance)
        {
            stateMachine.SwitchState(new EnemyStunnedState(stateMachine));
        }
    }
}
