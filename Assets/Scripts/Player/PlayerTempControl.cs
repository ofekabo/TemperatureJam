using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTempControl : TempControl
{
    public int tempDifference = 10;
    public float damageEachXTime = 1f;
    public int selfDamage;
    
    public event Action OnChangeTemp;
    public override void ChangeTemp(int amount)
    {
        base.ChangeTemp(amount);
        OnChangeTemp?.Invoke();
    }
}
