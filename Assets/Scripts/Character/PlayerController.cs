using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterStateMachine[] CharacterStateMachines { get; private set; }

    private void Start()
    {
        InputReader.OnChangeToKunoichiEvent += ChangeToKunoichi;
        InputReader.OnChangeToNinjaEvent += ChangeToNinja;
    }

    private void ChangeToKunoichi()
    {
        CharacterStateMachines[0].IsCurrent = true;
        CharacterStateMachines[1].IsCurrent = false;
    }

    private void ChangeToNinja()
    {
        CharacterStateMachines[0].IsCurrent = false;
        CharacterStateMachines[1].IsCurrent = true;
    }
}
