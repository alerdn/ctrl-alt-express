using System;
using UnityEngine;

public class WhiteFlameInterectable : MonoBehaviour, IInteractable
{
    public event Action OnFlamedLited;

    [SerializeField] private Transform _lookAt;
    [SerializeField] private MeshRenderer[] _renderers;

    public void Interact(CharacterStateMachine stateMachine)
    {
        CharacterChannelingState state = new CharacterChannelingState(stateMachine, transform);
        state.OnChannelingCompleted += LightWhiteFlame;

        stateMachine.SwitchState(state);
    }

    private void LightWhiteFlame()
    {
        foreach (MeshRenderer renderer in _renderers)
        {
            renderer.material.SetInt("_DECALEMISSIONONOFF", 1);
        }

        OnFlamedLited?.Invoke();
    }
}
