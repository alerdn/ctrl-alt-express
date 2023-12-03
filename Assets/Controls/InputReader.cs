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

    public Vector2 MovementValue { get; private set; }
    public Vector2 AimValue { get; private set; }

    private Controls _controls;

    private void OnEnable()
    {
        _controls = new Controls();
        _controls.Player.SetCallbacks(this);
        _controls.Player.Enable();
    }

    private void OnDisable()
    {
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

    public void OnActionn(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        OnActionEvent?.Invoke();
    }
}
