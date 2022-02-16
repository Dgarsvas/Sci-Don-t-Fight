using System;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : BaseAI
{
    [SerializeField] protected StateMachine _stateMachine;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected Animator animator;


    private void Update()
    {
        _stateMachine?.Tick();
    }

    public override void TakeDamage(float damage, Vector3 dir)
    {
        health -= damage;

        if (health <= 0)
        {
            rb.isKinematic = false;
            rb.AddForce(dir * 500f);
        }
    }

    public override void Despawn()
    {
        Destroy(gameObject);
    }
}