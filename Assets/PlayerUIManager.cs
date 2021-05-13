using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;


public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider tempSlider;
    [SerializeField] PlayerController player;

    private void Awake()
    {
        tempSlider = GetComponentInChildren<Slider>();
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }
    }

    private void Start()
    {

        GameEvents.Current.OnPlayerChangeTemp += UpdateTempSlider;
        GameEvents.Current.OnPlayerTakeDamage += UpdateHealthSlider;
        Invoke(nameof(UpdateTempSlider),0.01f);
        Invoke(nameof(UpdateHealthSlider),0.01f);
    }


    private void UpdateTempSlider()
    {
        tempSlider.value = player.tempControl.TempInPrecentage();
    }
    
    private void UpdateHealthSlider()
    {
        healthSlider.value = player.healthComp.HealthInPrecentage();
    }

    private void OnDestroy()
    {
        GameEvents.Current.OnPlayerChangeTemp -= UpdateTempSlider;
        GameEvents.Current.OnPlayerTakeDamage -= UpdateHealthSlider;
    }
}
