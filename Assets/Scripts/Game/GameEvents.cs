using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Current;

    private void Awake()
    {
        Current = this;
    }
    
    public event Action<int> onDoorWayTriggerEnter;

    public void DoorwayTriggerEnter(int id)
    {
        onDoorWayTriggerEnter?.Invoke(id);
    }
}
