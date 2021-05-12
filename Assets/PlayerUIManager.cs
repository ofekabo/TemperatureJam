using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;


public class PlayerUIManager : MonoBehaviour
{
    private Slider _tempSlider;
    [SerializeField] PlayerTempControl playerTempControl;

    private void Awake()
    {
        _tempSlider = GetComponentInChildren<Slider>();
        if (playerTempControl == null)
        {
            playerTempControl = FindObjectOfType<PlayerTempControl>();
        }
    }

    private void Start()
    {

        GameEvents.Current.OnPlayerChangeTemp += UpdateSlider;
        Invoke(nameof(UpdateSlider),0.01f);
    }


    private void UpdateSlider()
    {
        _tempSlider.value = playerTempControl.TempInPrecentage();
    }

    private void OnDestroy()
    {
        GameEvents.Current.OnPlayerChangeTemp -= UpdateSlider;
    }
}
