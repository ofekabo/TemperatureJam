using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempControl : MonoBehaviour
{
    [SerializeField] private int initTemp;
    [SerializeField] private int maxTemp = 50;
    [SerializeField] private int minTemp = 20;

    [Header("read only")]
    [SerializeField] private int _temperature;
    public int Temperature => _temperature;

    private TempUIOverhead _tempUI;

    private void Awake()
    {
        _tempUI = GetComponentInChildren<TempUIOverhead>();
    }

    private void Start()
    {
        _temperature = initTemp;
    }

    public void ChangeTemp(int amount,int deathTemp)
    {
        _temperature += amount;
        _temperature = Mathf.Clamp(_temperature, minTemp, maxTemp);
        _tempUI.UpdateText(deathTemp);
    }
    public void ChangeTemp(int amount)
    {
        _temperature += amount;
        _temperature = Mathf.Clamp(_temperature, minTemp, maxTemp);
    }
}