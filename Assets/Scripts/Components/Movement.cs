using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour, IRuntime
{
    [SerializeField] float moveSpeed = 5f;

    private Rigidbody2D _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void UpdateRuntime()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        Vector2 movement = new Vector2(h,v);
        
        // transform.Translate(movement * (Time.deltaTime * moveSpeed),Space.World);
        
        _rb.MovePosition(_rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    void AiLocomotion()
    {
        
    }
}
