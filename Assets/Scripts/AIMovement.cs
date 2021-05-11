using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIMovement : Movement
{
    [SerializeField] Transform target;
    [SerializeField] float nextWaypointDistance = 3f;

    Path _path;
    int _currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker _seeker;

    private float _initSpeed;

    public override void Start()
    {
        _seeker = GetComponent<Seeker>();
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
        if (_path == null)
        {
            return;
        }

        if (_currentWaypoint >= _path.vectorPath.Count)
        {
            reachedEndOfPath = true;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2) _path.vectorPath[_currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * (moveSpeed * Time.deltaTime);
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, _path.vectorPath[_currentWaypoint]);

        if (distance < nextWaypointDistance)
            _currentWaypoint++;
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