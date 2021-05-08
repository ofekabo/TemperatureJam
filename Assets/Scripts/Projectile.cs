using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    private Rigidbody2D _rb;
    [HideInInspector] public float speed;
    [HideInInspector] public float lifeTime;

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
}
