using UnityEngine;
using UnityEngine.AI;

public class IdleState : IState
{
    private Transform selfTransform;
    private Transform target;
    private GameObject attackProjectile;
    private Material signalMaterial;

    private Animator animator;


    public IdleState(Animator animator, Transform selfTransform, Material signalMaterial)
    {
        this.animator = animator;
        this.selfTransform = selfTransform;
        this.signalMaterial = signalMaterial;
    }

    public void OnEnter()
    {

        signalMaterial.SetColor("_EmissionColor", Color.yellow);
    }

    public void OnExit()
    {

    }

    public void Tick()
    {

    }

}
