using UnityEngine;
using UnityEngine.AI;

public class AttackState : IState
{
    private Transform selfTransform;
    private Transform target;
    private GameObject attackProjectile;
    private Material signalMaterial;

    private Animator animator;
    private const float ATTACK_COOLDOWN_TIME = 2f;
    private const float WHEN_TO_FIRE_PROJECTILE = 0.9f;
    private const uint STARTER_AMMO = 1;
    private Vector3 projectileSpawnOffset;
    private uint ammo;

    public AttackState(Animator animator, Transform chaseTarget, Transform selfTransform, GameObject projectile, Material signalMaterial)
    {
        this.animator = animator;
        target = chaseTarget;
        attackProjectile = projectile;
        this.selfTransform = selfTransform;
        this.signalMaterial = signalMaterial;

        // TODO: fix offset so projectile comes out of head and not back of head
        projectileSpawnOffset = new Vector3(selfTransform.position.x, selfTransform.position.y + 2.0f, selfTransform.position.z);
    }

    public void OnEnter()
    {
        ammo = STARTER_AMMO;
        signalMaterial.SetColor("_EmissionColor", Color.red);
    }

    public void OnExit()
    {
        animator.SetFloat("attackCooldown", ATTACK_COOLDOWN_TIME);
    }

    public void Tick()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Goblin Attack") && 
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= WHEN_TO_FIRE_PROJECTILE && ammo > 0)
        {
            ammo--;
            var projectile = GameObject.Instantiate(attackProjectile, selfTransform.position + projectileSpawnOffset, attackProjectile.transform.rotation);
            projectile.GetComponent<Projectile>().Launch(target.position);
        }
    }

}
