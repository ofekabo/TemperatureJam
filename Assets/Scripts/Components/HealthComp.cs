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
   
   private PlayerController _playerController;
   
   public int CurrentHealth => _currentHealth;

   private void Start()
   {
       _currentHealth = initHealth;
       _playerController = GetComponent<PlayerController>();
   }

   public event Action OnPlayerTakeDamage;
   
   public void TakeDamage(int damage)
   {
       if (_playerController._movement.playerCanGetDamage)
       {
           OnPlayerTakeDamage?.Invoke();
           _currentHealth -= damage;
           _playerController.tempControl.ActivateBlink();
           GameEvents.Current.CallPlayerUpdateHealth();
       }
   }

   public void AddHealth(int healthToAdd)
   {
       _currentHealth = Mathf.Min(_currentHealth + healthToAdd,maxHealth);
       GameEvents.Current.CallPlayerUpdateHealth();
   }
   
   public float HealthInPrecentage()
   {
       float currentPrecentage = (float)_currentHealth / maxHealth * 100f;
       return  currentPrecentage;
   }
   
}
