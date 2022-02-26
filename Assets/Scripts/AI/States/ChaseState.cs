using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IState
{
    private NavMeshAgent agent;
    private Transform target;

    public ChaseState(NavMeshAgent navMeshAgent, Transform chaseTarget)
    {
        agent = navMeshAgent;
        target = chaseTarget;
    }

    public void OnEnter()
    {
        agent.isStopped = false;
        agent.SetDestination(target.position);
    }

    public void OnExit()
    {
        agent.isStopped = true;
    }

    public void Tick()
    {
        agent.SetDestination(target.position);
    }
}
