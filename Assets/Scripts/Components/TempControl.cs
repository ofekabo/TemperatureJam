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


    private void Start()
    {
        _temperature = initTemp;
    }

    public void ChangeTemp(int amount)
    {
        _temperature += amount;
        _temperature = Mathf.Clamp(_temperature, minTemp, maxTemp);
        ShowTemp();
        if (_temperature > maxTemp)
        {
            
        }
        if (_temperature < minTemp)
        {
        }
    }

    private void ShowTemp()
    {
        print(_temperature);
    }
}