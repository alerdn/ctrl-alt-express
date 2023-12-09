using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(fileName = "InputReader", menuName = "Controls/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action OnSwitchCharacterEvent;
    public event Action OnActionEvent;
    public event Action OnSpecialAttackEvent;
    public event Action OnPauseEvent;

    public Vector2 MovementValue { get; private set; }
    public Vector2 AimValue { get; private set; }
    public bool IsAttacking { get; private set; }

    private Controls _controls;

    private void OnEnable()
    {
        _controls = new Controls();
        _controls.Player.SetCallbacks(this);
        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.SetCallbacks(null);
        _controls.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnSwitchCharacter(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        OnSwitchCharacterEvent?.Invoke();
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        OnActionEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsAttacking = true;
        }
        else if (context.canceled)
        {
            IsAttacking = false;
        }
    }

    public void OnSpecialAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        OnSpecialAttackEvent?.Invoke();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        OnPauseEvent?.Invoke();
    }
}
