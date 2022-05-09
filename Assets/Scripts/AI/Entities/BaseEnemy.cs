using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BaseEnemy : BaseEntity
{
   // [SerializeField] protected Rigidbody rb;
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Renderer brainRenderer;

    // freeze stuff
    [SerializeField] protected GameObject IceCube;
    protected float freezeTime;

    private bool isDead;

    protected StateMachine stateMachine;

    private void Update()
    {
        if (!isDead)
        {
            // this is garbage code but it worky
            if (freezeTime <= 0.0f)
            {
                stateMachine?.Tick();

                // hopefully I can do that here
                float cooldown = animator.GetFloat("attackCooldown");
                animator.SetFloat("attackCooldown",  cooldown - Time.deltaTime);

                navMeshAgent.isStopped = false;
                animator.speed = 1.0f;
            }
            else
            {
                freezeTime -= Time.deltaTime;
                navMeshAgent.isStopped = true;
                animator.speed = 0.0f;
            }
        }
    }

    public override void TakeDamage(float damage, Vector3 dir)
    {
        health -= damage;

        if (health <= 0)
        {
            isDead = true;
           // rb.isKinematic = false;
           // rb.AddForce(dir * 500f);
        }
    }

    public override void Despawn()
    {
        Destroy(gameObject);
    }

    public override void GetFrozen(float freezeTime)
    {
        this.freezeTime = freezeTime;
        var go = Instantiate(IceCube, transform);
        go.GetComponent<IceCubeBehaviour>().lifetime = freezeTime;
        //go.transform.position = transform.position + new Vector3(0.0f, 1.0f, 0.0f); // add offset for ice cube
    }
}