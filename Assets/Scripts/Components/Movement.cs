using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    private Rigidbody2D _rb;
    private PlayerController _player;

    private void Awake()
    {
        try
        {
            _player = GetComponent<PlayerController>();
        }
        catch (NullReferenceException e)
        {
            throw;
        }
        
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        
    }
    

    public void PlayerMovement(Animator animator)
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        Vector2 movement = new Vector2(h,v);
        if (Mathf.Abs(movement.magnitude) > 0)
        {
            animator.SetBool("IsMoving",true);
        }
        else
        {
            animator.SetBool("IsMoving",false);
        }

        _rb.MovePosition(_rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
        animator.SetFloat("Horizontal",h);
        animator.SetFloat("Vertical",v);
    }

    void AiLocomotion()
    {
        // ai movement
    }
}
