using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BondController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    private Character[] _characters;
    private LineRenderer _bondRenderer;

    private void Start()
    {
        _characters = _playerController.CharacterStateMachines.ToList().Select(x => x.Character).ToArray();
        _bondRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        CalculateDistance();
        DrawBond();
    }

    private void CalculateDistance()
    {
        float distance = Vector3.Distance(_characters[0].transform.position, _characters[1].transform.position);
        if (distance > 10f)
        {
            Debug.Log("Bond broken");
        }
    }

    private void DrawBond()
    {
        _bondRenderer.SetPosition(0, new Vector3(_characters[0].transform.position.x, .5f, _characters[0].transform.position.z));
        _bondRenderer.SetPosition(1, new Vector3(_characters[1].transform.position.x, .5f, _characters[1].transform.position.z));
    }
}
