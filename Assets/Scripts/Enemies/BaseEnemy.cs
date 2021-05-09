using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] int deathTemp;
    private TempControl _tempControl;

    private void Awake()
    {
        _tempControl = GetComponent<TempControl>();
    }

    void Start()
    {
        _tempControl.ChangeTemp(0,deathTemp);
    }

    private void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Projectile projectile = other.GetComponent<Projectile>();
        if (projectile)
        {
            if (projectile.bulletType == Projectile.Type.Ice)
                _tempControl.ChangeTemp(-6,deathTemp);
            
            if (projectile.bulletType == Projectile.Type.Lava)
                _tempControl.ChangeTemp(5,deathTemp);

            if (Mathf.Abs(_tempControl.Temperature - deathTemp) <= 1)
            {
               Destroy(gameObject);
            }
        }
    }
}