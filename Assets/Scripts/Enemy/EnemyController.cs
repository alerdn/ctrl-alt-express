using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [field: SerializeField] public PlayerController Player { get; private set; }

    [SerializeField] private Transform _enemyZoneParent;

    private void Start()
    {
        EnemyZone[] zones = _enemyZoneParent.GetComponentsInChildren<EnemyZone>();
        foreach (EnemyZone zone in zones)
        {
            zone.Init(Player.CharacterStateMachines);
        }
    }

}
