using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IState
{
    private NavMeshAgent agent;
    private Transform target;
    private Transform selfTransform;
    private Material signalMaterial;

    private const float CLOSE_ENOUGH = 5.0f;

    // TODO: since our enemy is a ranged based one it would make more sense if it wouldn't go to the exact position
    // where the player is but somewhere near
    public ChaseState(NavMeshAgent navMeshAgent, Transform chaseTarget, Material signalMaterial, Transform selfTransform)
    {
        agent = navMeshAgent;
        target = chaseTarget;
        this.signalMaterial = signalMaterial;
        this.selfTransform = selfTransform;
    }

    public void OnEnter()
    {
        agent.isStopped = false;
        if ((selfTransform.position - target.position).magnitude > CLOSE_ENOUGH)
            agent.SetDestination(target.position);

        signalMaterial.SetColor("_EmissionColor", Color.blue);
    }

    public void OnExit()
    {
        agent.isStopped = true;
    }

    public void Tick()
    {
        if((selfTransform.position - target.position).magnitude > CLOSE_ENOUGH)
            agent.SetDestination(target.position);

        selfTransform.LookAt(new Vector3(target.position.x, selfTransform.position.y, target.position.z), Vector3.up);
    }
}
