using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SOIntDisplay : MonoBehaviour
{
    [SerializeField] private SOInt _soInt;

    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        _soInt.OnValueChanged += OnValueChanged;
    }

    private void OnDestroy()
    {
        _soInt.OnValueChanged -= OnValueChanged;
    }

    private void OnValueChanged(int value)
    {
        _text.text = value.ToString();
    }
}
