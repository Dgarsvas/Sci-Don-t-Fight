using UnityEngine;
using System.Collections;

//https://vilbeyli.github.io/Projectile-Motion-Tutorial-for-Arrows-and-Missiles-in-Unity3D/

public class EnemyProjectile : Projectile
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float force;
    [SerializeField] private float damage;
    [Range(20.0f, 75.0f)] public float LaunchAngle;

    [SerializeField] private GameObject particles;

    private bool isFlying;
    private Quaternion initialRot;

    private void Awake()
    {
        isFlying = true;
        initialRot = transform.rotation;
        // instantiate particles here also
    }

    public override void Launch()
    {

    }


    public override void Launch(Vector3 targetPos)
    {
        Vector3 projectileXZPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
        Vector3 targetXZPos = new Vector3(targetPos.x, 0.0f, targetPos.z);

        // rotate the object to face the target
        transform.LookAt(targetXZPos);


        float R = Vector3.Distance(projectileXZPos, targetXZPos);
        float G = Physics.gravity.y;
        float tanAlpha = Mathf.Tan(LaunchAngle * Mathf.Deg2Rad);
        float H = (targetPos.y /*+ GetPlatformOffset()*/) - transform.position.y;

        // calculate initial speed required to land the projectile on the target object 
        float Vz = Mathf.Sqrt(G * R * R / (2.0f * (H - R * tanAlpha)));
        float Vy = tanAlpha * Vz;

        // create the velocity vector in local space and get it in global space
        Vector3 localVelocity = new Vector3(0f, Vy, Vz);
        Vector3 globalVelocity = transform.TransformDirection(localVelocity);

        // launch the object by setting its initial velocity and flipping its state
        rb.velocity = globalVelocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        isFlying = false;
        BaseEntity entity = collision.gameObject.GetComponent<BaseEntity>();
        if (entity != null)
        {
            entity.TakeDamage(damage, collision.impulse);
        }

        Instantiate(particles, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void Update()
    {
        if(isFlying)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity) * initialRot;
        }
    }
}
