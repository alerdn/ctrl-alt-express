using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public CharacterController Controller { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }

    [field: SerializeField] public Animator Animator { get; private set; }

    private void Start()
    {
        Controller = GetComponent<CharacterController>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }
}
