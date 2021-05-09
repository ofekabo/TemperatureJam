using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] int deathTemp;
    [SerializeField] int tempDifference = 3;
    [SerializeField] bool basicHealth = false;
    [HideInInspector]public TempControl tempControl;
    
    public int DeathTemp => deathTemp;
    public enum Type{Lava,Ice}
    public Type enemyType;

    private void Awake()
    {
        tempControl = GetComponent<TempControl>();
    }

    void Start()
    {
        tempControl.ChangeTempAI(0);
    }

    private void Update()
    {
       
    }
    

    public void UpdateTemp()
    {
        if (!basicHealth)
        {
            if (Mathf.Abs(tempControl.Temperature - deathTemp) <= tempDifference)
            {
                Destroy(gameObject);
            }
            return;
        }
            
        if (enemyType == Type.Lava && tempControl.Temperature < deathTemp)
        {
            Destroy(gameObject);
        }

        if (enemyType == Type.Ice && tempControl.Temperature > deathTemp)
        {
            Destroy(gameObject);
        }
        
        
    }
}