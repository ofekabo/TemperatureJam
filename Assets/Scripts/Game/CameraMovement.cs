using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{    
    
    /// <summary>
    /// Duplicate current doorway and place the previous room camera pos so it will work
    /// </summary>
    
    [Tooltip("Camera Cooldown must be bigger than {tweenTime} by 0.15f")]
    [SerializeField][Range(0.3f,1f)] float cameraCooldown = 0.8f;
    [SerializeField][Range(0.1f,1f)] float tweenTime = 0.2f;
    private float _cooldownInterval;
    private Transform _player;
    private void Start()
    {
        GameEvents.Current.OnDoorWayTriggerEnterCamera += MoveCameraToNextRoom;
        _player = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        _cooldownInterval += Time.deltaTime;
    }

    void  MoveCameraToNextRoom(Transform nextCamPos,Transform nextPlayerPos)
    {
        if (transform.position != new Vector3(nextCamPos.position.x,nextCamPos.position.y, -10) && _cooldownInterval > cameraCooldown)
        {
            LeanTween.move(gameObject,new Vector3 (nextCamPos.position.x,nextCamPos.position.y,-10),tweenTime);
            _player.position = nextPlayerPos.position;
            _cooldownInterval = 0;
        }
           
    }
}
