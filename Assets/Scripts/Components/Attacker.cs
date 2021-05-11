using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [HideInInspector] public Transform player;
    public float delayBetweenAttacks = 1f;
    public float distanceToAttack = 2f;
    public int damage = 5;
    [SerializeField] float returnBackDelay = 0.2f;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>().transform;
        }
    }

    [ContextMenu("Attack")]
    public IEnumerator Attack()
    {
        Vector2 originalPos = transform.position;
        Vector2 targetPos = player.position;
      
        
        _rb.AddForce((targetPos - originalPos).normalized * 15,ForceMode2D.Impulse);
        yield return new WaitForSeconds(returnBackDelay);
        _rb.AddForce((originalPos - (Vector2)transform.position).normalized * 5,ForceMode2D.Impulse);
        
    }
}
