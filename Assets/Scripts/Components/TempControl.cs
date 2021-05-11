using UnityEngine;

public class TempControl : MonoBehaviour
{
    [SerializeField] private int initTemp;
    [SerializeField] private int maxTemp = 50;
    [SerializeField] private int minTemp = 20;

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