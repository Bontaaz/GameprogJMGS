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

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        Vector3 direction = new Vector3(horizontal, vertical,0);
        Move(direction);
        
    }
    private void Move(Vector3 speed)
    {
        transform.position += speed/20;
    }

}
 


   
