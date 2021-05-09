using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;


public class Projectile : MonoBehaviour
{


    private Rigidbody2D _rb;
    [HideInInspector] public float speed;
    [HideInInspector] public float lifeTime;
    public enum Type
    {
        Ice,
        Lava
    }

    public Type bulletType;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = transform.right * speed;
    }

    private void Update()
    {
        
        lifeTime -= Time.deltaTime;
        
        if (lifeTime > 0) { return; }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BaseEnemy enemy = other.GetComponent<BaseEnemy>();
        TempControl temp = other.GetComponent<TempControl>();
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
            
            enemy.UpdateTemp();
            Destroy(gameObject);
        }
    }
}
