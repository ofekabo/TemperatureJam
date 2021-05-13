using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [HideInInspector] public Transform player;
    public float delayBetweenAttacks = 1f;
    public float distanceToAttack = 2f;
    [Tooltip("Temperature changed applied to temp control based on ice/lava enemy type")]
    public int tempDamage = 5;
    [Tooltip("Damage applied to player health component")]
    public int healthDamage = 5;
    [SerializeField] float returnBackDelay = 0.2f;
    private Rigidbody2D _rb;
    private BaseEnemy _enemy;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _enemy = GetComponent<BaseEnemy>();
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>().transform;
        }
    }

    public void Attack()
    {
        StartCoroutine("AttackCo");
    }
    [ContextMenu("Attack")]
    public IEnumerator AttackCo()
    {
        Vector2 originalPos = transform.position;
        Vector2 targetPos = player.position;
      
        _enemy.animator.animator.enabled = false;
        _enemy.movement.DontMove();
        yield return new WaitForSeconds(0.1f);
        _rb.AddForce((targetPos - originalPos).normalized * 15,ForceMode2D.Impulse);
        yield return new WaitForSeconds(returnBackDelay);
        _rb.AddForce((originalPos - (Vector2)transform.position).normalized * 5,ForceMode2D.Impulse);
        StopCoroutine("AttackCo");
        _enemy.animator.animator.enabled = enabled;
        _enemy.movement.Move();
    }
}
