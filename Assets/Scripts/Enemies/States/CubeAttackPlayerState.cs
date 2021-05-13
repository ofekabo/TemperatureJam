using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAttackPlayerState : MonsterState
{
    private Attacker _attacker;
    private float _attackInterval;
    
    public MonsterStateID GetID()
    {
        return MonsterStateID.AttackPlayer;
    }

    public void Enter(BaseEnemy enemy)
    {
        _attacker = enemy.GetComponent<Attacker>();
    }

    public void Update(BaseEnemy enemy)
    {
        Debug.Log(DistanceCheck(enemy));
        _attackInterval += Time.deltaTime; 
        if (_attackInterval > _attacker.delayBetweenAttacks && DistanceCheck(enemy))
        {
            _attacker.Attack();
            _attackInterval = 0;
        }
    }

    public void Exit(BaseEnemy enemy)
    {
       
    }

    bool DistanceCheck(BaseEnemy enemy)
    {
        var dist = Vector2.Distance(enemy.player.position,enemy.transform.position);
        
        return dist < _attacker.distanceToAttack;
    }
}
