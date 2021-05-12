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
        _attackInterval += Time.deltaTime;
        if (_attackInterval > _attacker.delayBetweenAttacks && DistanceCheck(enemy))
        {
            _attacker.StartCoroutine(nameof(_attacker.Attack));
            _attackInterval = 0;
        }
    }

    public void Exit(BaseEnemy enemy)
    {
       
    }

    bool DistanceCheck(BaseEnemy enemy)
    {
        float dist = (enemy.player.position - enemy.transform.position).sqrMagnitude;
        
        if (dist < _attacker.distanceToAttack * _attacker.distanceToAttack)
        {
            return true;
        }
        return false;
    }
}
