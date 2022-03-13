using UnityEngine;
using UnityEngine.AI;

public class AttackState : IState
{
    private Transform target;
    private Animator animator;
    private const float ATTACK_COOLDOWN_TIME = 5f;

    public AttackState(Animator animator, Transform chaseTarget)
    {
        this.animator = animator;
        target = chaseTarget;
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
        animator.SetFloat("attackCooldown", ATTACK_COOLDOWN_TIME);
    }

    public void Tick()
    {
    }
}
