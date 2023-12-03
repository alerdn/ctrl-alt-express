using UnityEngine;

public class WhiteFlameInterectable : MonoBehaviour, IInteractable

{
    [SerializeField] private Transform _lookAt;

    public void Interact(CharacterStateMachine stateMachine)
    {
        CharacterChannelingState state = new CharacterChannelingState(stateMachine, _lookAt);
        state.OnChannelingCompleted += LightWhiteFlame;

        stateMachine.SwitchState(state);
    }

    private void LightWhiteFlame()
    {
        Debug.Log("Lighting white flame");
    }
}
