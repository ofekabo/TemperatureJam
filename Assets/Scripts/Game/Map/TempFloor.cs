using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class TempFloor : MonoBehaviour
{
    TilemapCollider2D _tilemap;
    [SerializeField] int tempDamageToAi = 3;
    [SerializeField] int tempDamageToPlayer = 5;
    [SerializeField] bool damagePlayerHealth = false;
    [SerializeField] int healthDamageToPlayer = 5;
    [SerializeField] private float overTimeEffect = 0.5f;

    public enum Type
    {
        Lava,
        Ice
    }

    public Type FloorType;

    bool doDmg;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var tmp = other.GetComponent<TempControl>();
        var health = other.GetComponent<HealthComp>();
        if (tmp || health) ;
        {
            doDmg = true;
            StartCoroutine(TempChange(tmp, health));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var tmp = other.GetComponent<TempControl>();
        var health = other.GetComponent<HealthComp>();
        if (tmp || health) ;
        {
            doDmg = false;
            StopCoroutine(TempChange(tmp, health));
        }
    }


    IEnumerator TempChange(TempControl tmp, HealthComp health)
    {
        while (doDmg)
        {
            switch (FloorType)
            {
                case Type.Ice:
                    tmp.ChangeTemp(-tempDamageToPlayer);
                    tmp.ChangeTempAI(-tempDamageToAi);
                    break;

                case Type.Lava:
                    tmp.ChangeTemp(tempDamageToPlayer);
                    tmp.ChangeTempAI(tempDamageToAi);
                    break;
            }

            if (health && damagePlayerHealth)
            {
                health.TakeDamage(healthDamageToPlayer);
            }

            yield return new WaitForSeconds(overTimeEffect);
        }
    }
}