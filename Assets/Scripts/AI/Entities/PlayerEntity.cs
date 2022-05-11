using UnityEngine;
using System.Collections;

public class PlayerEntity : BaseEntity
{
    [SerializeField] Transform respawnPoint;
    public override void Despawn() // more like respawn
    {
        if (respawnPoint)
            gameObject.transform.position = respawnPoint.position;
        else
            Debug.LogWarning("no respawn point, pls assign");
    }

    public override void GetFrozen(float freezeTime)
    {
        // not used
    }

    public override void TakeDamage(float damage, Vector3 direction)
    {
        Despawn();
    }
}
