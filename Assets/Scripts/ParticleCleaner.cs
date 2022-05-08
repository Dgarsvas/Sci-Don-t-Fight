using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCleaner : MonoBehaviour
{
    private ParticleSystem ps;


    public void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void FixedUpdate()
    {
        if (ps && !ps.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
