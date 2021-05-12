using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5f;

    protected Rigidbody2D rb;
    private PlayerController _player;
    private BaseEnemy _enemy;

    public void Awake()
    {
        try
        {
            _player = GetComponent<PlayerController>();
            _enemy = GetComponent<BaseEnemy>();
        }
        catch (NullReferenceException e)
        {
            Debug.Log(e);
        }

        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Start()
    {
    }


    public void PlayerMovement(Animator animator)
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(h, v);
        if (Mathf.Abs(movement.magnitude) > 0)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
        animator.SetFloat("Horizontal", h);
        animator.SetFloat("Vertical", v);
    }

    public virtual void AiLocomotion()
    {
        // ai movement
    }
}