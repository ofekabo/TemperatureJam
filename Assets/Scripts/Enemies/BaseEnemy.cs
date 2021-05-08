using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    private TempControl _tempControl;

    // Start is called before the first frame update
    void Start()
    {
        _tempControl = GetComponent<TempControl>();
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
                _tempControl.ChangeTemp(-5);
            if (projectile.bulletType == Projectile.Type.Lava)
                _tempControl.ChangeTemp(5);
        }
    }
}