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
    
    public event Action<int> OnDoorwayTriggerEnterSpawner;
    public event Action<Transform> OnDoorWayTriggerEnterCamera;

    public void DoorwayTriggerEnter(int id,Transform nextCamPos)
    {
        OnDoorwayTriggerEnterSpawner?.Invoke(id);
        OnDoorWayTriggerEnterCamera?.Invoke(nextCamPos);
    }

    #region PlayerEvents
    public event Action OnPlayerChangeTemp;

    public void CallPlayerChangeTemp()
    {
        OnPlayerChangeTemp?.Invoke();
    }
    public event Action OnPlayerTakeDamage;

    public void CallPlayerUpdateHealth()
    {
        OnPlayerTakeDamage?.Invoke();
    }
    
    public event Action OnPlayerShot;

    public void CallCameraShake()
    {
        OnPlayerShot?.Invoke();
    }
        

    #endregion

    #region EnemiesEvents
    
    public event Action<Transform> OnEnemyDeath;
    
    public void CallEnemyDeath(Transform transform)
    {
        OnEnemyDeath?.Invoke(transform);
    }
    

    #endregion
    
}
