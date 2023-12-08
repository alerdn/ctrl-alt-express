using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    public bool DialogueFinished { get; private set; }

    [SerializeField] private GameObject _dialogueMenu;
    [SerializeField] private TMP_Text _textBox;
    [SerializeField] private string[] _texts;

    private int _textIndex;

    public void ShowDialogue()
    {
        _dialogueMenu.SetActive(true);
        StartCoroutine(TypeText());

    }

    public void CloseDialogue()
    {
        _dialogueMenu.SetActive(false);
    }

    private IEnumerator TypeText()
    {

        foreach (string text in _texts)
        {
            _textBox.text = "";
            foreach (char letter in text.ToCharArray())
            {
                if (Keyboard.current.anyKey.wasPressedThisFrame || Gamepad.current.buttonSouth.wasPressedThisFrame)
                {
                    _textBox.text = text;
                    break;
                }

                _textBox.text += letter;
                yield return new WaitForSeconds(.05f);
            }

            yield return new WaitForSeconds(1f);
            _textIndex++;
        }

        DialogueFinished = true;
        CloseDialogue();
    }
}
