using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    private Camera _camera;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        _camera = Camera.main;
    }
    
    public void CalculateAngleForAnim(Vector2 pos)
    {
        float angleBetween = AngleBetweenVector2(pos);
        
        if (angleBetween >= 45 && angleBetween < 135)
        {
            //top
            animator.SetFloat("LookAt",1);
        }
        else if (angleBetween >= 135 || angleBetween < -135)
        {
            //left
            animator.SetFloat("LookAt",2);
        }
        else if (angleBetween >= -135 && angleBetween < -45)
        {
            //down
            animator.SetFloat("LookAt",0);
        }
        else if (angleBetween >= -45 && angleBetween < 45)
        {
            //right
            animator.SetFloat("LookAt",3);
        }
    }
    private float  AngleBetweenVector2(Vector2 pos)
    {
        Vector2 dir = Input.mousePosition - _camera.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        return angle;
    }


}
