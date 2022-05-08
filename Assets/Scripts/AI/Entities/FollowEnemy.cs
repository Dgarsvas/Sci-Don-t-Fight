using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// name of this is prolly not very good for what we do here now
public class FollowEnemy : BaseEnemy
{
    [SerializeField] private FieldOfView fov;

    [SerializeField] private Transform player;
    [SerializeField] private GameObject projectile;

    void Awake()
    {
        fov.OnEnterView += PlayerEnteredFOV;
        fov.OnExitView += PlayerExitedFOV;

        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        stateMachine = new StateMachine();

        var chaseState = new ChaseState(navMeshAgent, player, brainRenderer, transform);
        var attackState = new AttackState(animator, player, transform, projectile, brainRenderer);
        var idleState = new IdleState(navMeshAgent, animator, transform, brainRenderer);

        stateMachine.AddTransition(chaseState, attackState, () => { return animator.GetFloat("attackCooldown") <= Mathf.Epsilon; });
        // transition should happen when attack animation stops playing. This is the best I could manage to implement
        stateMachine.AddTransition(attackState, chaseState, () => { return animator.GetCurrentAnimatorStateInfo(0).IsName("Goblin Attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f; });
        stateMachine.AddTransition(chaseState, idleState, () => { return !animator.GetBool("isAlert"); });
        stateMachine.AddTransition(attackState, idleState, () => { return !animator.GetBool("isAlert"); });
        stateMachine.AddTransition(idleState, chaseState, () => { return animator.GetBool("isAlert"); });


        // initial state should be idle
        stateMachine.SetState(idleState);
    }

    void PlayerEnteredFOV()
    {
        animator.SetBool("isAlert", true);
    }

    void PlayerExitedFOV()
    {
        animator.SetBool("isAlert", false);
    }


}