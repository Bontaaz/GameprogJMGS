using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 1;
    public int niveau;
    
   

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Mouse X")*GameManager.instance.ReturnSens();
        float vertical = Input.GetAxisRaw("Mouse Y")* GameManager.instance.ReturnSens();
        if (horizontal == 0 || vertical == 0)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }

       
        
        
        
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        Vector3 direction = new Vector3(horizontal, vertical,0);
        Move(direction);
        
    }
    private void Move(Vector3 speed)
    {
        transform.position += speed/20;
    }

}
 


   
