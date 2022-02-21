using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEntity : MonoBehaviour
{
    public float health;

    public abstract void TakeDamage(float damage, Vector3 direction);

    public abstract void Despawn();
}
