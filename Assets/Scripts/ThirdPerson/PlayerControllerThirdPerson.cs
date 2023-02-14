using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerThirdPerson : MonoBehaviour
{
    public float forwardSpeed = 5000f;
    public float strafeSpeed = 2000f;
    public float turnSpeed = 1000f;
    public LayerMask groundMask;
    public float airDamping = 0.1f;
    
    private Transform groundCheck;
    public float groundDistance = 0.4f;
    private Rigidbody rb;

    void Start()
    {
        groundCheck = transform.Find("GroundCheck");
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float turnAmount =  turnSpeed * Time.deltaTime * Input.GetAxis("Mouse X");
        transform.Rotate(transform.up, turnAmount);
        
        bool grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //Debug.Log(grounded);
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveForce =  forwardSpeed * vertical * Time.deltaTime * transform.forward + 
                             strafeSpeed * horizontal * Time.deltaTime * transform.right;        
        if (grounded)
        {

            rb.AddForce(moveForce);
            
            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector3.up * 1000f);    
            }
        }
        else // in air
        {
            rb.AddForce(moveForce * airDamping);
        }
        
    }

}