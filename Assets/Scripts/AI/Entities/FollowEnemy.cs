using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : BaseEnemy
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject projectile;

    void Awake()
    {
        stateMachine = new StateMachine();

        var chaseState = new ChaseState(navMeshAgent, player);
        var attackState = new AttackState(base.animator, player, transform, projectile);

        stateMachine.AddTransition(chaseState, attackState, () => { return animator.GetFloat("attackCooldown") <= Mathf.Epsilon; });
        // transition should happen when attack animation stops playing. This is the best I could manage to implement
        stateMachine.AddTransition(attackState, chaseState, () => { return animator.GetCurrentAnimatorStateInfo(0).IsName("Goblin Attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f; });

        // initial state should be idle
        stateMachine.SetState(chaseState);
    }
}