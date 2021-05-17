using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public Movement _movement;
    Shooter _shooter;
    AnimatorController _animator;
    [HideInInspector] public PlayerTempControl tempControl;
    [HideInInspector] public HealthComp healthComp;

    [Header("Dash")] [SerializeField] float dashForce = 50;
    [SerializeField] float dashCooldown = 1f;
    [SerializeField] [Range(0.1f, 0.8f)] float invulnerableTime = 0.2f;

    float dashInterval;

    bool _holdingShift;
    Camera _mainCam;
    bool _isCoroutineRunning;

    [Header("Hit Sound")]
    [SerializeField] AudioClip hitSFX;
    private AudioSource _as;

    void Start()
    {
        _movement = GetComponent<Movement>();
        _shooter = GetComponent<Shooter>();
        _animator = GetComponent<AnimatorController>();
        tempControl = GetComponent<PlayerTempControl>();
        healthComp = GetComponent<HealthComp>();
        _as = GetComponent<AudioSource>();
        _mainCam = Camera.main;
        tempControl.OnChangeTemp += Hit;
        healthComp.OnPlayerTakeDamage += PlayHitSound;
    }


    void Update()
    {
        _movement.PlayerMovement(_animator.animator);
        _shooter.UpdateRuntime();
        _animator.CalculateAngleForAnim(Input.mousePosition, _mainCam.WorldToScreenPoint(transform.position));
        _shooter.AimWeapons(_holdingShift);


        #region Input

        if (Input.GetMouseButtonDown(0))
        {
            _shooter.LavaShot();
            tempControl.ChangeTemp(5);
        }

        if (Input.GetMouseButtonDown(1))
        {
            _shooter.IceShot();
            tempControl.ChangeTemp(-5);
        }

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            GameEvents.Current.CallCameraShake();

        dashInterval += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashInterval > dashCooldown)
            {
                _movement.isDashing = true;
                StartCoroutine(_movement.PlayerDash(dashForce, invulnerableTime));
                dashInterval = 0;
            }
        }


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _holdingShift = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _holdingShift = false;
        }

        #endregion
    }

    private void Hit()
    {
        if (healthComp.CurrentHealth <= 0)
        {
            GameEvents.Current.CallPlayerDied();
        }

        if (Mathf.Abs(tempControl.Temperature - tempControl.minTemp) <= tempControl.tempDifference
            ||
            Mathf.Abs(tempControl.Temperature - tempControl.maxTemp) <= tempControl.tempDifference)
        {
            if (!_isCoroutineRunning)
            {
                StartCoroutine(nameof(ReduceHealthEachXTime));
                _isCoroutineRunning = true;
            }
        }
        else
        {
            StopCoroutine(nameof(ReduceHealthEachXTime));
            _isCoroutineRunning = false;
        }
    }

    IEnumerator ReduceHealthEachXTime()
    {
        while (true)
        {
            healthComp.TakeDamage(tempControl.selfDamage);
            yield return new WaitForSeconds(tempControl.damageEachXTime);
        }
    }


    void PlayHitSound()
    {
        _as.PlayOneShot(hitSFX);
    }


    private void OnDestroy()
    {
        tempControl.OnChangeTemp -= Hit;
        healthComp.OnPlayerTakeDamage -= PlayHitSound;
    }
}