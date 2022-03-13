using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody), typeof(Animator))]
public class BaseEnemy : BaseEntity
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected Animator animator;
    private bool isDead;

    protected StateMachine stateMachine;

    private void Update()
    {
        if (!isDead)
        {
            stateMachine?.Tick();

            // hopefully I can do that here
            float cooldown = animator.GetFloat("attackCooldown");
            animator.SetFloat("attackCooldown",  cooldown - Time.deltaTime);
        }
    }

    public override void TakeDamage(float damage, Vector3 dir)
    {
        health -= damage;

        if (health <= 0)
        {
            isDead = true;
            rb.isKinematic = false;
            rb.AddForce(dir * 500f);
        }
    }

    public override void Despawn()
    {
        Destroy(gameObject);
    }
}