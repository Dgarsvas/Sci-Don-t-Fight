using UnityEngine;
using System.Collections;

public class PlayerEntity : BaseEntity
{
    [SerializeField] Transform respawnPoint;
    public override void Despawn() // more like respawn
    {
        gameObject.SetActive(false);
        PauseMenuController.Instance.PlayerDied(gameObject);
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
