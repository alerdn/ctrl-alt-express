using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [field: SerializeField] public PlayerController Player { get; private set; }

    [SerializeField] private Transform _enemyZoneParent;

    [SerializeField] private Boss _boss;

    private void Start()
    {
        _boss.Init(Player.CharacterStateMachines);

        EnemyZone[] zones = _enemyZoneParent.GetComponentsInChildren<EnemyZone>();
        foreach (EnemyZone zone in zones)
        {
            zone.Init(Player.CharacterStateMachines);
            zone.OnZoneCompleted += _boss.OnZoneCompleted;
        }
    }

}
