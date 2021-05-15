using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : BaseEnemy
{
    private Shooter _shooter;
   
    public override void Awake()
    {
        base.Awake();
        _shooter = GetComponent<Shooter>();
    }

    private void Start()
    {
        _shooter.OnFired += PlayShootAnim;
    }

    public override void Update()
    {
        _shooter.AiAimWeapons(0,player.position,transform.position);
        base.Update();
    }

    public override void RegisterStates()
    { 
        StateMachine = new MonsterStateMachine(this);
        StateMachine.RegisterState(new AIShooterState());
    }

    private void PlayShootAnim()
    {
        animator.animator.SetTrigger("Fire");
    }
}
