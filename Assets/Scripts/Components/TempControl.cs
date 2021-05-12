using System;
using UnityEngine;

public class TempControl : MonoBehaviour
{   
    [Header("Read Only")]
    [SerializeField] private int temperature;
    public int Temperature { get => temperature; }

    [Header("Temp Control")]
    [SerializeField] private int initTemp;
    public  int maxTemp = 50;
    public int minTemp = 20;

  
    

    [HideInInspector]public TempUIOverhead tempUI;

    private void Awake()
    {
        tempUI = GetComponentInChildren<TempUIOverhead>();
    }

    private void Start()
    {
        temperature = initTemp;
        if(!tempUI) { return; }
        tempUI.UpdateText();
    }

    public void ChangeTempAI(int amount)
    {
        temperature += amount;
        temperature = Mathf.Clamp(temperature, minTemp, maxTemp);
        if(tempUI)
            tempUI.UpdateText();
    }
    public virtual void ChangeTemp(int amount)
    {
        temperature += amount;
        temperature = Mathf.Clamp(temperature, minTemp, maxTemp);
    }
    
}