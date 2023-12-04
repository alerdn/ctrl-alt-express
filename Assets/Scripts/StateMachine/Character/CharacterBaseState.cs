using System;
using UnityEngine;

public abstract class CharacterBaseState : State
{
    protected readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
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

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Character.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected Vector3 CalculeMovement()
    {
        Vector3 forward = stateMachine.MainCamera.transform.forward;
        Vector3 right = stateMachine.MainCamera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y
            + right * stateMachine.InputReader.MovementValue.x;
    }
}
