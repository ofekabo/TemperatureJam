using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour , IRuntime
{
    Animator _animator;
    private Camera _camera;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _camera = Camera.main;
    }

    
    public void UpdateRuntime()
    {

        CalculateAngleForAnim();
    }
     
    private void CalculateAngleForAnim()
    {
        
        float angleBetween = AngleBetweenVector2();
        
        if (angleBetween >= 45 && angleBetween < 135)
        {
            //top
            _animator.SetTrigger("top");
        }
        else if (angleBetween >= 135 || angleBetween < -135)
        {
            //left
            _animator.SetTrigger("left");
        }
        else if (angleBetween >= -135 && angleBetween < -45)
        {
            //down
            _animator.SetTrigger("down");
        }
        else if (angleBetween >= -45 && angleBetween < 45)
        {
            //right
            _animator.SetTrigger("right");
        }
        
    }
    private float  AngleBetweenVector2()
    {
        Vector2 dir = Input.mousePosition - _camera.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        return angle;
    }


}
