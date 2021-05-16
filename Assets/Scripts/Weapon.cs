using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    [SerializeField] Transform shootingPoint;

    public BaseProjectile bulletProjectile;

    // public BaseProjectile iceBulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletLifeTime = 1.5f;

    AudioSource _as;
    
    private void Start()
    {
        _as = GetComponent<AudioSource>();
    }

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
        _as.PlayOneShot(bullet.shotSFX);
    }
}