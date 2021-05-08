using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour , IRuntime
{
    Animator _animator;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    
    public void UpdateRuntime()
    {

        CalculateAngleForAnim();
    }
     
    private void CalculateAngleForAnim()
    {
        
        float angleBetween = AngleBetweenVector2();
     
        _animator.SetFloat("Angle",angleBetween);
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
        
        Debug.Log(angleBetween);
    }
    private float  AngleBetweenVector2()
    {
        Vector2 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        return angle;
    }


}
