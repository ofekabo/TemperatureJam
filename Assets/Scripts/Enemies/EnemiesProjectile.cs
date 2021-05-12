using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemiesProjectile : BaseProjectile
{
    public int healthDamage;
    public int tempDamage;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController p = other.GetComponent<PlayerController>();
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        TempControl temp = other.GetComponent<TempControl>();
        HealthComp pHealth = other.GetComponent<HealthComp>();
        if (p)
        {
           
            if (bulletType == Type.Ice)
            {
                temp.ChangeTemp(-tempDamage);
            }

            if (bulletType == Type.Lava)
            {
                temp.ChangeTemp(tempDamage);
            }

            if (rb)
            {
                Vector2 dir = (rb.position - projectileInitPos).normalized;
                rb.AddForce(dir * force);
            }

            pHealth.TakeDamage(healthDamage);
        }
        
        SpawnEffectNDestroy();
    }
}

