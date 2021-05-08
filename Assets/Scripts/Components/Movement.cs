using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour, IRuntime
{
    [SerializeField] float moveSpeed = 5f;
    public void UpdateRuntime()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        Vector2 movement = new Vector2(h,v);
        
        transform.Translate(movement * (Time.deltaTime * moveSpeed),Space.World);
    }

    void AiLocomotion()
    {
        
    }
}
