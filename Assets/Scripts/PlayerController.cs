using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 0.01f;
    public float rotateSpeed = 100f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * moveZ;// + transform.right * moveX;
        controller.Move(move * moveSpeed);
        transform.Rotate(Vector3.up, moveX * rotateSpeed * Time.deltaTime);

        if (controller.isGrounded)
        {
            velocity.y = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = jumpForce;
            }
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}