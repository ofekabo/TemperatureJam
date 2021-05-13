using System;
using UnityEngine;

public class TempControl : MonoBehaviour
{   
    [Header("Read Only")]
    [SerializeField] private int temperature;

    [Header("Hit Effect")]
    [SerializeField] float blinkDuration = 0.15f;
    [SerializeField] float blinkIntensity = 10f;
    private float _blinkTimer = 0;
    Material _mat;
   
    
    [Header("Temp Control")]
    [SerializeField] private int initTemp;
    public  int maxTemp = 50;
    public int minTemp = 20;

    [HideInInspector]public TempUIOverhead tempUI;
    public int Temperature { get => temperature; }
    
    
    private void Awake()
    {
        tempUI = GetComponentInChildren<TempUIOverhead>();
    }

    private void Start()
    {
        temperature = initTemp;
        _mat = GetComponent<SpriteRenderer>().material;
        
        if(!tempUI) { return; }
        tempUI.UpdateText();
    }

    private void Update()
    {
        DoBlinkEffect();
    }

    public void ChangeTempAI(int amount)
    {
        temperature += amount;
        temperature = Mathf.Clamp(temperature, minTemp, maxTemp);
        if(tempUI)
            tempUI.UpdateText();
        
        ActivateBlink();
    }
    public virtual void ChangeTemp(int amount)
    {
        temperature += amount;
        temperature = Mathf.Clamp(temperature, minTemp, maxTemp);
    }

    public void ActivateBlink()
    {
        _blinkTimer = blinkDuration;
    }

    private void DoBlinkEffect()
    {
        _blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(_blinkTimer / blinkDuration);
        float intesnity = (lerp * blinkIntensity) +1f;
        _mat.SetColor("_Color", Color.white * intesnity);
    }
    
}