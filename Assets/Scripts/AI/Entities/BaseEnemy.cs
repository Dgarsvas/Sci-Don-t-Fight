using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody))]
public class BaseEnemy : BaseEntity
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected NavMeshAgent navMeshAgent;
    private bool isDead;

    protected StateMachine stateMachine;

    private void Update()
    {
        if (!isDead)
        {
            stateMachine?.Tick();
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