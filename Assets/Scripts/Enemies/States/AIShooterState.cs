using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooterState : MonsterState
{
    private Shooter _shooter;
    
    public MonsterStateID GetID()
    {
       return MonsterStateID.AttackPlayer;
    }

    public void Enter(BaseEnemy enemy)
    {
        _shooter = enemy.GetComponent<Shooter>();
    }

    public void Update(BaseEnemy enemy)
    {
        _shooter.UpdateRuntime();
        
        switch (enemy.enemyType)
        {
            case BaseEnemy.Type.Ice:
                _shooter.IceShot(); 
                break;
            
            case BaseEnemy.Type.Lava:
                _shooter.LavaShot();
                break;
        }
    }

    public void Exit(BaseEnemy enemy)
    {
        
    }
}
