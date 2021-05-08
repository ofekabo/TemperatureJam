using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement adjustments")]
    [SerializeField] float turnSpeed = 5f;
    Movement _movement;
    
    Shooter _shooter;
    bool _holdingShift;

    void Start()
    {
        _movement = GetComponent<Movement>();
        _shooter = GetComponent<Shooter>();
    }
    
    
    void Update()
    {
        _movement.UpdateRuntime();
        
        
        _shooter.PlayerAim(turnSpeed);
        _shooter.UpdateRuntime();
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
