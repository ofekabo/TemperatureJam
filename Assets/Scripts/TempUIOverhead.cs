using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;


public class TempUIOverhead : MonoBehaviour
{
   TempControl _tempControl;
   [SerializeField] TMP_Text tempText;
    
    void Start()
    {
        _tempControl = transform.root.GetComponent<TempControl>();
    }

    public void UpdateText(int deathTemp)
    {
        tempText.text = $"{_tempControl.Temperature}/{deathTemp}";
    }
}
