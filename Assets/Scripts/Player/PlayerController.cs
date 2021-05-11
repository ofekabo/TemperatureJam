using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Movement _movement;
    Shooter _shooter;
    [HideInInspector]public TempControl tempControl;
    AnimatorController _animator;

    bool _holdingShift;

    void Start()
    {
        _movement = GetComponent<Movement>();
        _shooter = GetComponent<Shooter>();
        _animator = GetComponent<AnimatorController>();
        tempControl = GetComponent<TempControl>();
    }


    void Update()
    {
        _movement.PlayerMovement(_animator.animator);
        _shooter.UpdateRuntime();
        _animator.CalculateAngleForAnim(Input.mousePosition, Camera.main.WorldToScreenPoint(transform.position));

        _shooter.AimWeapons(_holdingShift);

        if (Input.GetMouseButtonDown(0))
        {
            _shooter.LavaShot();
            tempControl.ChangeTemp(5);
        }

        if (Input.GetMouseButtonDown(1))
        {
            _shooter.IceShot();
            tempControl.ChangeTemp(-5);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _holdingShift = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _holdingShift = false;
        }
    }

    public void Hit()
    {
        if (tempControl.Temperature < tempControl.minTemp || tempControl.Temperature > tempControl.maxTemp)
        {
            //die
        }
    }
    
}