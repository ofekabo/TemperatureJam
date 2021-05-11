using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform shootingPoint;

    public PlayerProjectile bulletProjectile;

    // public PlayerProjectile iceBulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletLifeTime = 1.5f;

    public enum weaponType
    {
        Lava,
        Ice
    };

    public weaponType WeaponType;

    public void Shoot()
    {
        var bullet = Instantiate(bulletProjectile, shootingPoint.position, shootingPoint.rotation);
        bullet.speed = bulletSpeed;
        bullet.lifeTime = bulletLifeTime;
    }
}