using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable]
public struct DialogueData
{
    public string Name;
    public string Text;
}

public class DialogueUI : MonoBehaviour
{
    public bool DialogueFinished { get; private set; }
    [field: SerializeField] public UnityEvent TriggerDialogue { get; private set; }

    [SerializeField] private GameObject _dialogueMenu;
    [SerializeField] private TMP_Text _textBox;
    [SerializeField] private TMP_Text _speaker;
    [SerializeField] private DialogueData[] _texts;

    private int _textIndex;

    public void ShowDialogue(DialogueData[] data = null)
    {
        if (data != null)
            _texts = data;
        _dialogueMenu.SetActive(true);
        StartCoroutine(TypeText());
    }

    public void CloseDialogue()
    {
        _dialogueMenu.SetActive(false);
    }

    private IEnumerator TypeText()
    {
        foreach (DialogueData dialoge in _texts)
        {
            _speaker.text = dialoge.Name;
            string text = dialoge.Text;

            _textBox.text = "";
            foreach (char letter in text.ToCharArray())
            {
                if (Keyboard.current.anyKey.wasPressedThisFrame || (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame))
                {
                    _textBox.text = text;
                    break;
                }

                _textBox.text += letter;
                yield return new WaitForSeconds(.025f);
            }

            yield return new WaitForSeconds(1f);
            _textIndex++;
        }

        DialogueFinished = true;
        CloseDialogue();
    }
}
