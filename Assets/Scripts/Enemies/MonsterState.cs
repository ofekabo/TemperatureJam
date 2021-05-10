using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum MonsterStateID
{
    AttackPlayer,
    Death,
}

public interface MonsterState
{
   MonsterStateID GetID();
   
   void Enter(BaseEnemy enemy);
   void Update(BaseEnemy enemy);
   void Exit(BaseEnemy enemy);
   
}
