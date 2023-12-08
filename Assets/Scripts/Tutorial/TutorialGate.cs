using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGate : MonoBehaviour
{
    public event Action OnCharactersPassed;

    private bool _charactersPassed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_charactersPassed)
                return;

            _charactersPassed = true;
            OnCharactersPassed?.Invoke();
        }
    }
}
