using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private void Start()
    {
        GameEvents.Current.OnDoorWayTriggerEnterCamera += MoveCameraToNextRoom;
    }


    private void MoveCameraToNextRoom(Transform nextCamPos)
    {
        transform.position = new Vector3(nextCamPos.position.x,nextCamPos.position.y,-10);
    }
}