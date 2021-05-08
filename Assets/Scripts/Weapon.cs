using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform shootingPoint;
    [SerializeField] Projectile lavaBulletPrefab;
    [SerializeField] Projectile iceBulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletLifeTime = 1.5f;

    public enum weaponType {Lava,Ice};
 
    public weaponType WeaponType;

    public void Shoot()
    {
        switch (WeaponType)
        {
            case weaponType.Ice:
                var bulletIce = Instantiate(iceBulletPrefab,shootingPoint.position,shootingPoint.rotation);
                bulletIce.speed = bulletSpeed;
                bulletIce.lifeTime = bulletLifeTime;
                break;
            
            case weaponType.Lava:
                var bulletLava = Instantiate(lavaBulletPrefab,shootingPoint.position,shootingPoint.rotation);
                bulletLava.speed = bulletSpeed;
                bulletLava.lifeTime = bulletLifeTime;
                break;
        }

    }
}
