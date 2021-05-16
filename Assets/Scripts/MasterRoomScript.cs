using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MasterRoomScript : MonoBehaviour
{
    [SerializeField]int maxKeys;
    private int _currentKeys;

    private void Start()
    {
        GameEvents.Current.OnRoomCleared += AddKey;
    }

    void AddKey()
    {
        _currentKeys++;
        if (_currentKeys >= maxKeys)
        {
            Debug.Log("room opened"+_currentKeys);
        }
    }
}
