using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class Movement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5f;

    protected Rigidbody2D rb;
    private PlayerController _player;
    private BaseEnemy _enemy;
    
    float h;
    float v;
    Vector2 movement;
    [HideInInspector]public bool isDashing = false;
    [HideInInspector]public bool playerCanGetDamage = true;

    public void Awake()
    {
        isDashing = false;
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
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        movement = new Vector2(h, v);
        if (Mathf.Abs(movement.magnitude) > 0)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
        
        if(!isDashing)
            rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
        
        animator.SetFloat("Horizontal", h);
        animator.SetFloat("Vertical", v);
    }

    public IEnumerator PlayerDash(float dashSpeed,float invulnerableTime)
    {
        rb.velocity = Vector2.zero;
        playerCanGetDamage = false;
        yield return new WaitForSeconds(0.02f);
        rb.AddForce(movement.normalized * dashSpeed,ForceMode2D.Impulse);
        StartCoroutine(ResetMovement(invulnerableTime));
    }

    IEnumerator ResetMovement(float invulnerableTime)
    {
        yield return new WaitForSeconds(0.10f);
        isDashing = false;
        yield return new WaitForSeconds(invulnerableTime - 0.10f);
        playerCanGetDamage = true;
    }

    public virtual void AiLocomotion()
    {
        // ai movement
    }
}