using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController control;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    private Vector3 vel;
    private bool grounded;
    
    // Update is called once per frame
    void Update()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (grounded && vel.y < 0)
        {
            vel.y = -2f;
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        
        
        Vector3 move = transform.right * x + transform.forward * z;
        control.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump"))
        {
            vel.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        vel.y += gravity * Time.deltaTime;
        control.Move(vel * Time.deltaTime);

    }
}
