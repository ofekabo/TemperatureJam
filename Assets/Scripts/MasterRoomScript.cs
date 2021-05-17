using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MasterRoomScript : MonoBehaviour
{
    [SerializeField]int maxKeys;
    [SerializeField] GameObject[] doors;
    [SerializeField] Sprite openDoorSprite;
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
            foreach (var door in doors)
            {
                door.GetComponent<Collider2D>().enabled = false;
                door.GetComponent<SpriteRenderer>().sprite = openDoorSprite;
            }
           
        }
    }
}
