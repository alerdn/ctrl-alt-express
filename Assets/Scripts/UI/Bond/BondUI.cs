using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BondUI : MonoBehaviour
{
    [SerializeField] private Image _bondProgressBar;
    [SerializeField] private Transform _bondChargeParent;
    [SerializeField] private GameObject _bondChargePrefab;

    [SerializeField] private BondStateMachine _bondStateMachine;

    private List<GameObject> _bondCharges = new();

    private void Start()
    {
        _bondStateMachine.Bond.OnValueChanged += UpdateBond;
        _bondStateMachine.BondCharge.OnValueChanged += UpdateBondCharge;

        UpdateBond(_bondStateMachine.Bond.Value);
        UpdateBondCharge(_bondStateMachine.BondCharge.Value);
    }

    private void OnDestroy()
    {
        _bondStateMachine.Bond.OnValueChanged -= UpdateBond;
        _bondStateMachine.BondCharge.OnValueChanged -= UpdateBondCharge;
    }

    private void UpdateBond(int value)
    {
        _bondProgressBar.DOFillAmount((float)value / 100f, 0.1f);
    }

    private void UpdateBondCharge(int value)
    {
        int difference = value - _bondCharges.Count;

        if (difference > 0)
        {
            for (int i = 0; i < difference; i++)
            {
                GameObject bondCharge = Instantiate(_bondChargePrefab, _bondChargeParent);
                _bondCharges.Add(bondCharge);
            }
        }
        else if (difference < 0)
        {
            for (int i = 0; i < Mathf.Abs(difference); i++)
            {
                GameObject bondCharge = _bondCharges[^1];
                _bondCharges.Remove(bondCharge);
                Destroy(bondCharge);
            }
        }
    }
}
