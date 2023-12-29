using System;
using UnityEngine;

public class WhiteFlameInterectable : MonoBehaviour, IInteractable
{
    public event Action OnFlamedLited;

    [SerializeField] private bool _showTutorial;
    [SerializeField] private DialogueData[] _dialogue;
    [SerializeField] private Transform _lookAt;
    [SerializeField] private MeshRenderer[] _renderers;

    private bool _isFlameAlreadyLited;

    public bool Interact(CharacterStateMachine stateMachine)
    {
        if (_isFlameAlreadyLited) return false;
        _isFlameAlreadyLited = true;

        CharacterChannelingState state = new CharacterChannelingState(stateMachine, transform);
        state.OnChannelingCompleted += LightWhiteFlame;

        stateMachine.SwitchState(state);
        return true;
    }

    public void LightWhiteFlame()
    {
        foreach (MeshRenderer renderer in _renderers)
        {
            renderer.material.SetInt("_DECALEMISSIONONOFF", 1);
        }

        if (_showTutorial)
        {
            DialogueManager.Instance.StartDialogue(_dialogue);
        }
        OnFlamedLited?.Invoke();
    }
}
