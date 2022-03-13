using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : BaseEnemy
{
    [SerializeField] private Transform player;

    void Awake()
    {
        stateMachine = new StateMachine();

        var chaseState = new ChaseState(navMeshAgent, player);
        var attackState = new AttackState(base.animator, player);

        stateMachine.AddTransition(chaseState, attackState, () => { return animator.GetFloat("attackCooldown") <= Mathf.Epsilon; });
        stateMachine.AddTransition(attackState, chaseState, () => { return animator.GetCurrentAnimatorStateInfo(0).IsName("Goblin Attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f; });

        // initial state should be idle
        stateMachine.SetState(chaseState);

        //stateMachine
    }
}