using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Movement _movement;
    Shooter _shooter;
    AnimatorController _animator;
    [HideInInspector]public PlayerTempControl tempControl;
    [HideInInspector] public HealthComp healthComp;

    bool _holdingShift;
    Camera _mainCam;
    bool _isCoroutineRunning;

    void Start()
    {
        _movement = GetComponent<Movement>();
        _shooter = GetComponent<Shooter>();
        _animator = GetComponent<AnimatorController>();
        tempControl = GetComponent<PlayerTempControl>();
        healthComp = GetComponent<HealthComp>();
        _mainCam = Camera.main;
        tempControl.OnChangeTemp += Hit;
    }


    void Update()
    {
        _movement.PlayerMovement(_animator.animator);
        _shooter.UpdateRuntime();
        _animator.CalculateAngleForAnim(Input.mousePosition, _mainCam.WorldToScreenPoint(transform.position));

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

    private void Hit()
    {
        
        if (healthComp.CurrentHealth <= 0)
        {
            //die
        }
        
        if (Mathf.Abs(tempControl.Temperature - tempControl.minTemp) <= tempControl.tempDifference
        ||
        Mathf.Abs(tempControl.Temperature - tempControl.maxTemp) <= tempControl.tempDifference)
        {
            if (!_isCoroutineRunning)
            {
                StartCoroutine(nameof(ReduceHealthEachXTime));
                _isCoroutineRunning = true;
            }
                
        }
        else
        {
            StopCoroutine(nameof(ReduceHealthEachXTime));
            _isCoroutineRunning = false;
        }
    }

    IEnumerator ReduceHealthEachXTime()
    {
        while (true)
        {
<<<<<<< Updated upstream
            healthComp.TakeDamage(tempControl.selfDamage);
=======
            healthComp.TakeDamage(2);
>>>>>>> Stashed changes
            yield return new WaitForSeconds(tempControl.damageEachXTime);
        }
    }

    private void OnDestroy()
    {
        tempControl.OnChangeTemp -= Hit;
    }
}