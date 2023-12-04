using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public CharacterController Controller { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }
    public int FreeLookBlendTreeHash { get; private set; }

    [field: SerializeField] public Animator Animator { get; private set; }
    [SerializeField] private string _freeLookBlendTreeName = "FreeLookBlendTreeKunoichi";

    private void Start()
    {
        Controller = GetComponent<CharacterController>();
        NavMeshAgent = GetComponent<NavMeshAgent>();

        FreeLookBlendTreeHash = Animator.StringToHash(_freeLookBlendTreeName);
    }
}
