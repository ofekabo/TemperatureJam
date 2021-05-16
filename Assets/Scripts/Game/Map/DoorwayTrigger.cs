using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cache = UnityEngine.Cache;

public class DoorwayTrigger : MonoBehaviour
{
    /// <summary>
    /// Write the id of the spawner u want to activate , can activate few at a time.
    /// </summary>
    [SerializeField] int id;

    [SerializeField] private float cameraSize = 8f;
    [SerializeField] Transform nextCameraPos;
    private Camera mainCamera;
    

    private void Start()
    {
        mainCamera  = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController p = other.GetComponent<PlayerController>();
        if (p)
        {
            GameEvents.Current.DoorwayTriggerEnter(id, nextCameraPos);
            if (Math.Abs(cameraSize - mainCamera.orthographicSize) > 0.01f)
            {
                LeanTween.value(mainCamera.orthographicSize, cameraSize, 1f).setOnUpdate(value =>
                {
                    mainCamera.orthographicSize = value;
                });
            }
        }
    }
}