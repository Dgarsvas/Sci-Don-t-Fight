using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolderProjectile : Projectile
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float force;
    [SerializeField] private float damage;

    [SerializeField] private GameObject particles;

    private void Awake()
    {
        rb.velocity = transform.forward * force;
    }

    public override void Launch()
    {
        
    }

    public override void Launch(Vector3 pos)
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        BaseEntity entity = collision.gameObject.GetComponent<BaseEntity>();
        if (entity != null)
        {
            entity.TakeDamage(damage, collision.impulse);
        }

        Instantiate(particles, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
