using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerProjectile : BaseProjectile
{
    [Header("Explosion")]
    [SerializeField] float explosiveForce = 500f;
    [SerializeField] GameObject diffExplosive;
    [SerializeField] GameObject sameExplosive;
    [SerializeField] float explosionRadius = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {

        BaseEnemy enemy = other.GetComponent<BaseEnemy>();
        TempControl temp = other.GetComponent<TempControl>();
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (temp && enemy)
        {
            if (bulletType == Type.Ice)
            {
                temp.ChangeTempAI(-6);
            }

            if (bulletType == Type.Lava)
            {
                temp.ChangeTempAI(5);
            }

            if (rb)
            {
                Vector2 dir = (rb.position - projectileInitPos).normalized;
                rb.AddForce(dir * force);
            }

            enemy.UpdateTemp();
        }
        
        CheckCollision(other,diffExplosive,sameExplosive,explosionRadius,explosiveForce);
        
        
    }
}
