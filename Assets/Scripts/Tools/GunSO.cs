using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Usables/Gun")]
public class GunSO : BaseUsableSO
{
    [SerializeField] private float rechargeTime;
    [SerializeField] private GameObject bullet;
    private float curRecharge;
    private Transform shootPoint;

    public override void Setup(Transform point)
    {
        shootPoint = point;
    }

    public override void StartUse(UsageEnum usageType)
    {
        curRecharge = 0;
    }

    public override void ContinueUse(UsageEnum usageType)
    {
        if (curRecharge <= 0)
        {
            Shoot();
        }
    }

    public override void BeingHeld()
    {
        curRecharge -= Time.deltaTime;
    }

    public override void EndUse(UsageEnum usageType)
    {

    }

    private void Shoot()
    {
        Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        bullet.GetComponent<Projectile>().Launch();
        curRecharge = rechargeTime;
    }

    public override Color GetColor()
    {
        throw new NotImplementedException();
    }
}
