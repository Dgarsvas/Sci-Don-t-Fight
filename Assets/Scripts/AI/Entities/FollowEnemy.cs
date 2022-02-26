using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : BaseEnemy
{
    [SerializeField] private Transform player;

    void Awake()
    {
        stateMachine = new StateMachine();
        stateMachine.SetState(new ChaseState(navMeshAgent, player));
    }
}