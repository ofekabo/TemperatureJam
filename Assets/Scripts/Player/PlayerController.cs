using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement adjustments")]
    [SerializeField] float turnSpeed = 5f;
    
    Movement _movement;
    Shooter _shooter;
    AnimatorController _animator;
    
    bool _holdingShift;

    void Start()
    {
        _movement = GetComponent<Movement>();
        _shooter = GetComponent<Shooter>();
        _animator = GetComponent<AnimatorController>();
    }
    
    
    void Update()
    {
       
        _movement.UpdateRuntime();
        _shooter.UpdateRuntime();
        _animator.UpdateRuntime();
        
        _shooter.PlayerAim(turnSpeed);

        
        _shooter.AimWeapons(_holdingShift);

        if (Input.GetMouseButtonDown(0))
        { 
            _shooter.LavaShot();
        }
        if(Input.GetMouseButtonDown(1))
        { 
            _shooter.IceShot();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _holdingShift = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            _holdingShift = false;
        }
    }

}
