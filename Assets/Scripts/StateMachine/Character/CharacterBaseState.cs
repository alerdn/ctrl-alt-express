using System;
using UnityEngine;

public abstract class CharacterBaseState : State
{
    protected readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    protected readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    protected const float AnimatorDampTime = .075f;

    protected CharacterStateMachine stateMachine;

    public CharacterBaseState(CharacterStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.Character.transform.rotation = Quaternion.Lerp(
            stateMachine.Character.transform.rotation,
            Quaternion.LookRotation(movement),
            deltaTime * stateMachine.RotationDamping
        );
    }
}
