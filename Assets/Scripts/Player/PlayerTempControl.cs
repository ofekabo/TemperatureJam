using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTempControl : TempControl
{
<<<<<<< Updated upstream
    public int tempDifference = 10;
    public float damageEachXTime = 1f;
    public int selfDamage;
    
    public event Action OnChangeTemp;
    public override void ChangeTemp(int amount)
    {
        base.ChangeTemp(amount);
        OnChangeTemp?.Invoke();
    }
=======
   [Tooltip("The difference the player has to maintain between min and max temperature")]
   public int tempDifference = 10;
   [Tooltip("Deal the X damage to self if reached the difference temperature")]
   public int selfDamage = 2;
   [Tooltip("Deal damage to self each X time passed")]
   public float damageEachXTime = 1f;

   public event Action OnChangeTemp;
   public override void ChangeTemp(int amount)
   {
      base.ChangeTemp(amount);
      OnChangeTemp?.Invoke();
   }
>>>>>>> Stashed changes
}
