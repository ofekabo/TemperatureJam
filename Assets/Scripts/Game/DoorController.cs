using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] int id;
    private void Start()
    {
        GameEvents.Current.onDoorWayTriggerEnter += onDoorwayOpen;
    }

    private void onDoorwayOpen(int id)
    {
        if (id == this.id)
        {
            // do smth
        }
    }
}
