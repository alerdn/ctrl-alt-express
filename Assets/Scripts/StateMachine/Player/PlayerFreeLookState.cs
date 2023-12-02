using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private const float AnimatorDampTime = .1f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {

    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculeMovement();

        stateMachine.CurrentCharacter.Controller.Move(stateMachine.FreeLookMovement * deltaTime * movement);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.CurrentCharacter.Animator.SetFloat(FreeLookSpeedHash, 0f, AnimatorDampTime, deltaTime);
            return;
        }

        stateMachine.CurrentCharacter.Animator.SetFloat(FreeLookSpeedHash, 1f, AnimatorDampTime, deltaTime);
        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {

    }

    private Vector3 CalculeMovement()
    {
        Vector3 forward = stateMachine.MainCameraTrasform.forward;
        Vector3 right = stateMachine.MainCameraTrasform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y
            + right * stateMachine.InputReader.MovementValue.x;
    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.CurrentCharacter.transform.rotation = Quaternion.Lerp(stateMachine.CurrentCharacter.transform.rotation,
         Quaternion.LookRotation(movement),
         deltaTime * stateMachine.RotationDamping
        );
    }
}
