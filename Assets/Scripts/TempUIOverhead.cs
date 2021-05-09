using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;


public class TempUIOverhead : MonoBehaviour
{
    BaseEnemy _baseEnemy;
    [SerializeField] TMP_Text tempText;

    private void Awake()
    {
        _baseEnemy = GetComponentInParent<BaseEnemy>();
    }

    void Start()
    {
        
    }

    public void UpdateText()
    {
        tempText.text = $"{_baseEnemy.tempControl.Temperature}/{_baseEnemy.DeathTemp}";
    }
}
