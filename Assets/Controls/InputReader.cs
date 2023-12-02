using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(fileName = "InputReader", menuName = "Controls/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action OnChangeToKunoichiEvent;
    public event Action OnChangeToNinjaEvent;

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

    public void OnChangeToKunoichi(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        OnChangeToKunoichiEvent?.Invoke();
    }

    public void OnChengeToNinja(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        OnChangeToNinjaEvent?.Invoke();
    }
}
