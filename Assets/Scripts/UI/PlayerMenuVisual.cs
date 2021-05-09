using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuVisual : MonoBehaviour
{
    Shooter _shooter;
    Animator _animator;

    private void Awake()
    {
        _shooter = GetComponent<Shooter>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _animator.enabled = false;
        Invoke("EnableAniamtor",24);
    }

    void EnableAniamtor()
    {
        _animator.enabled = true;
    }

    private void Update()
    {
        _shooter.UpdateRuntime();
        
        _shooter.AimWeapons(false);

        if (Input.GetMouseButtonDown(0))
        {
            _shooter.LavaShot();

        }

        if (Input.GetMouseButtonDown(1))
        {
            _shooter.IceShot();

        }
    }
    
    
}
