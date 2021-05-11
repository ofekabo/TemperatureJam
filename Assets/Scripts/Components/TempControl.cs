using UnityEngine;

public class TempControl : MonoBehaviour
{
    [SerializeField] private int initTemp;
    public  int maxTemp = 50;
    public int minTemp = 20;

    [Header("Read Only")]
    [SerializeField] private int temperature;
    public int Temperature => temperature;

    public TempUIOverhead tempUI;

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
        tempUI.UpdateText();
    }
    public void ChangeTemp(int amount)
    {
        temperature += amount;
        temperature = Mathf.Clamp(temperature, minTemp, maxTemp);
    }
}