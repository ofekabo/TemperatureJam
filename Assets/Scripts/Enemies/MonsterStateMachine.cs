using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStateMachine
{
    public MonsterState[] states;
    public BaseEnemy enemy;
    public MonsterStateID currentState;

    public MonsterStateMachine(BaseEnemy enemy)
    {
        this.enemy = enemy;
        int numStates = System.Enum.GetNames(typeof(MonsterStateID)).Length;
        states = new MonsterState[numStates];
    }

    public void RegisterState(MonsterState state)
    {
        int index = (int)state.GetID();
        states[index] = state;
    }

    public MonsterState GetState(MonsterStateID stateID)
    {
        int index = (int)stateID;
        return states[index];
    }

    public void Update()
    {
        GetState(currentState)?.Update(enemy);
    }

    public void ChangeState(MonsterStateID newState)
    {
        GetState(currentState)?.Exit(enemy);
        currentState = newState;
        GetState(currentState)?.Enter(enemy);
    }
    
}
