using UnityEngine;
using UnityEngine.AI;

public class IdleState : IState
{
    private NavMeshAgent agent;
    private Transform selfTransform;
    private Transform target;
    private GameObject attackProjectile;
    private Material signalMaterial;

    private Animator animator;


    public IdleState(NavMeshAgent navMeshAgent, Animator animator, Transform selfTransform, Renderer brainRenderer)
    {
        this.agent = navMeshAgent;
        this.animator = animator;
        this.selfTransform = selfTransform;
        this.signalMaterial = brainRenderer.material;
    }

    public void OnEnter()
    {
        agent.isStopped = false;
        signalMaterial.SetColor("_EmissionColor", Color.yellow * 3.0f);

        Debug.Log(GlobalNavigationController.Instance.enabled);

        var navCon = GlobalNavigationController.Instance;
        if (navCon.hasPoints)
        {
            var target = GlobalNavigationController.Instance.GetClosestPoint(selfTransform.position);
            agent.SetDestination(target);
        }
    }

    public void OnExit()
    {

    }

    public void Tick()
    {
        var navCon = GlobalNavigationController.Instance;
        if (navCon.hasPoints)
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    agent.SetDestination(GlobalNavigationController.Instance.GetNextPoint(agent.destination));
                }
            }
        }
    }

}
