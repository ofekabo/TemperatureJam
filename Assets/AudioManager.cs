using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _as;
   [SerializeField] AudioClip explosionSound;
   [SerializeField] AudioClip healthpickupSound;
    void Start()
    {
        _as = GetComponent<AudioSource>();
        GameEvents.Current.OnExplosion += PlayExplosionSound;
        GameEvents.Current.OnHealthPickup += PlayHealthPickupSound;
    }

    void PlayExplosionSound()
    {
        _as.PlayOneShot(explosionSound);
    }
    void PlayHealthPickupSound()
    {
        _as.PlayOneShot(healthpickupSound);
    }

    private void OnDestroy()
    {
        GameEvents.Current.OnExplosion -= PlayExplosionSound;
        GameEvents.Current.OnHealthPickup -= PlayHealthPickupSound;
    }
}
