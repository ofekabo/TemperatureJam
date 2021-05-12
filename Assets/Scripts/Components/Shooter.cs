using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Shooter : MonoBehaviour, IRuntime
{
    const int Lava = 0;
    const int Ice = 1;

    [Tooltip("Each X seconds a projectile will be fired")] [SerializeField]
    float fireRate;

    [SerializeField] float bulletPushForce;

    private float _lavafireInterval;
    private float _icefireInterval;
    [SerializeField]private Weapon[] _weapons;

    #region Player

    private Camera _cam;
    private Vector3 _stoppedMousePos;

    #endregion

    private void Start()
    {
        _cam = Camera.main;
        _weapons = GetComponentsInChildren<Weapon>();
        foreach (var wep in _weapons)
        {
            wep.bulletProjectile.force = bulletPushForce;
        }
    }

    public void UpdateRuntime()
    {
        _lavafireInterval += Time.deltaTime;
        _icefireInterval += Time.deltaTime;
    }

    public void LavaShot()
    {
        if (_lavafireInterval > fireRate)
        {
            _weapons[Lava].Shoot();
            _lavafireInterval = 0;
        }
    }

    public void IceShot()
    {
        if (_icefireInterval > fireRate)
        {
            _weapons[Ice].Shoot();
            _icefireInterval = 0;
        }
    }


    #region Aim

    Quaternion LookAtTarget(Vector3 targetPos ,Vector3 currentPos)
    {
        Vector2 dir = targetPos - currentPos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        return targetRotation;
    }

    public void AimWeapons(bool holdingShift)
    {
        _weapons[Lava].transform.parent.rotation = LookAtTarget(Input.mousePosition, _cam.WorldToScreenPoint(transform.position));

        if (!holdingShift)
        {
            _weapons[Ice].transform.parent.rotation = LookAtTarget(Input.mousePosition, _cam.WorldToScreenPoint(transform.position));
            _stoppedMousePos = Input.mousePosition;
        }
        
        if (holdingShift)
        {
            _weapons[Ice].transform.parent.rotation = LookAtTarget(_stoppedMousePos, _cam.WorldToScreenPoint(transform.position));
        }
    }
    
    public void AiAimWeapons(int index,Vector2 targetPos, Vector2 currentPos)
    {
        _weapons[index].transform.rotation = LookAtTarget(targetPos,currentPos);

       
    }
    

    #endregion
}