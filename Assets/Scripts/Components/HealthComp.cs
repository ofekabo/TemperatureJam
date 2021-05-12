using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class HealthComp : MonoBehaviour
{
   [SerializeField] int initHealth = 50;
   public int maxHealth = 100;
   private int _currentHealth;
   
   public int CurrentHealth => _currentHealth;

   private void Start()
   {
       _currentHealth = initHealth;
   }

   public void TakeDamage(int damage)
   {
        _currentHealth -= damage;  
   }
}
