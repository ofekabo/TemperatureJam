using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorwayTrigger : MonoBehaviour
{
    /// <summary>
    /// Write the id of the spawner u want to activate , can activate few at a time.
    /// </summary>
    [SerializeField] int id;
    [SerializeField] Transform nextCameraPos;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController p = other.GetComponent<PlayerController>();
        if (p)
        {
            GameEvents.Current.DoorwayTriggerEnter(id,nextCameraPos);
        }
    }
}
