using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healthToAdd;
    private void OnTriggerEnter2D(Collider2D other)
    {
        HealthComp pHealth = other.GetComponent<HealthComp>();
        if (pHealth)
        {
            if (pHealth.CurrentHealth < pHealth.maxHealth)
            {
                GameEvents.Current.CallOnHealthPickup();
                pHealth.AddHealth(15);
                Destroy(gameObject);
            }
            
        }
    }
}
