using System;
using System.Collections;
using System.Collections.Generic;
<<<<<<< Updated upstream
using System.ComponentModel;
=======
>>>>>>> Stashed changes
using UnityEngine;

public class HealthComp : MonoBehaviour
{
<<<<<<< Updated upstream
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
=======
    [SerializeField] int initHealth;
    [SerializeField] int maxHealth;
    private int _currentHealth;
    
    
    public int CurrentHealth => _currentHealth;
    private void Start()
    {
        _currentHealth = initHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth,0,maxHealth);
    }
    
    public void TakeDamage(int damage,TempControl tempControl)
    {
        _currentHealth -= damage;
        tempControl.tempUI.UpdateText();
    }
>>>>>>> Stashed changes
}
