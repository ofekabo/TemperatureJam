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
    private Weapon[] _weapons;

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


    #region Player

    Quaternion LookAtMouse(Vector3 mousePos)
    {
        Vector2 dir = mousePos - _cam.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        return targetRotation;
    }

    public void AimWeapons(bool holdingShift)
    {
        _weapons[Lava].transform.parent.rotation = LookAtMouse(Input.mousePosition);

        if (!holdingShift)
        {
            _weapons[Ice].transform.parent.rotation = LookAtMouse(Input.mousePosition);
            _stoppedMousePos = Input.mousePosition;
        }


        if (holdingShift)
        {
            _weapons[Ice].transform.parent.rotation = LookAtMouse(_stoppedMousePos);
        }
    }

    #endregion
}