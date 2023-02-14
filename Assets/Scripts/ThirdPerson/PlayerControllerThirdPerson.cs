using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerThirdPerson : MonoBehaviour
{
    public float moveSpeed = 500f;
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
        Debug.Log(grounded);
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        if (grounded)
        {
            Vector3 moveForce = moveSpeed * (vertical * Time.deltaTime * transform.forward + horizontal * Time.deltaTime * transform.right);
            rb.AddForce(moveForce);
            
            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector3.up * 1000f);    
            }
        }
        else // in air
        {
            Vector3 moveForce = moveSpeed * (vertical * Time.deltaTime * transform.forward + horizontal * Time.deltaTime * transform.right);
            rb.AddForce(moveForce * airDamping);

        }
        
    }

}