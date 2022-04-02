using UnityEngine;
using UnityEngine.AI;

public class AttackState : IState
{
    private Transform selfTransform;
    private Transform target;
    private GameObject attackProjectile;

    private Animator animator;
    private const float ATTACK_COOLDOWN_TIME = 5f;
    private const float WHEN_TO_FIRE_PROJECTILE = 0.5f;
    private const uint STARTER_AMMO = 1;
    private Vector3 projectileSpawnOffset;
    private uint ammo;

    public AttackState(Animator animator, Transform chaseTarget, Transform selfTransform, GameObject projectile)
    {
        this.animator = animator;
        target = chaseTarget;
        attackProjectile = projectile;
        this.selfTransform = selfTransform;

        projectileSpawnOffset = new Vector3(selfTransform.position.x, selfTransform.position.y + 2.0f, selfTransform.position.z);
    }

    public void OnEnter()
    {
        ammo = STARTER_AMMO;
    }

    public void OnExit()
    {
        animator.SetFloat("attackCooldown", ATTACK_COOLDOWN_TIME);
    }

    public void Tick()
    {
        // lets assume attack animation has started and is set
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= WHEN_TO_FIRE_PROJECTILE && ammo > 0)
        {
            ammo--;
            var projectile = GameObject.Instantiate(attackProjectile, selfTransform.position + projectileSpawnOffset, attackProjectile.transform.rotation);
            projectile.GetComponent<Projectile>().Launch(target.position);
        }
    }

}
