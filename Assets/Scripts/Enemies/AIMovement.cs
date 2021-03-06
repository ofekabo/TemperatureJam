using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Pathfinding;
using Debug = UnityEngine.Debug;

public class AIMovement : Movement
{
    [SerializeField] Transform target;
    [SerializeField] float stoppingDistance = 0.0f;
    [SerializeField] float nextWaypointDistance = 3f;
    
    [Header("Debugging")]
    [SerializeField] bool drawGizmos = false;

    Path _path;
    int _currentWaypoint = 0;

    Seeker _seeker;

    private float _initSpeed;

    public override void Start()
    {
        _seeker = GetComponent<Seeker>();
        target = GetComponent<BaseEnemy>().player;
        
        InvokeRepeating("UpdatePath", 0.0f, 0.5f);
        _initSpeed = moveSpeed;
    }

    private void UpdatePath()
    {
        if (_seeker.IsDone())
            _seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            _path = p;
            _currentWaypoint = 0;
        }
    }

    public override void AiLocomotion()
    {
        if (_path == null) { return; }
        
        float dist = (target.position - transform.position).sqrMagnitude;
        
        if (dist < stoppingDistance * stoppingDistance)
        {
            DontMove();
        }
        else{ Move(); }
        
        if (_currentWaypoint >= _path.vectorPath.Count) { return; }
        

        Vector2 direction = ((Vector2) _path.vectorPath[_currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * (moveSpeed * Time.deltaTime);
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, _path.vectorPath[_currentWaypoint]);

        if (distance < nextWaypointDistance)
            _currentWaypoint++;
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = new Vector4(1,0,0,0.3f);
            Gizmos.DrawCube(transform.position,Vector2.one * stoppingDistance);
        }
    }


    // controlled by Animations events
    public void Move()
    {
        moveSpeed = _initSpeed;
    }


    public void DontMove()
    {
        moveSpeed = 0;
    }
}