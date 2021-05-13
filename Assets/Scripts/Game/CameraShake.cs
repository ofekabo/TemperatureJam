using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float magntidue = 4f;
    [SerializeField] float roughness = 2f;
    [SerializeField] float fadeIn = 0.5f;
    [SerializeField] float fadeOut = 0.5f;
    private void Start()
    {
        GameEvents.Current.OnPlayerShot += ShakeCamera;
    }
    

    void ShakeCamera()
    {
        CameraShaker.Instance.ShakeOnce(magntidue,roughness,fadeIn,fadeOut);
    }
}
