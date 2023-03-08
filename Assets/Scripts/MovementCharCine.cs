using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCharCine : MonoBehaviour
{
    public CharacterController control;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public Transform cam;
    
    private Vector3 yvel;
    private bool grounded;
    private float angleVelocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    // Update is called once per frame
    void Update()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        Vector3 inputAmount = new Vector3(horiz, 0f, vert);
        
        Vector3 moveAmount = Vector3.zero;

        if (inputAmount.magnitude > 0.1f)
        {
            float inputAngle = Mathf.Atan2(inputAmount.x, inputAmount.z) * Mathf.Rad2Deg;
            //Update inputAngle to move relative to camera angle
            inputAngle += cam.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, inputAngle, ref angleVelocity, 0.1f);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            moveAmount = Quaternion.Euler(0f, inputAngle, 0f) * Vector3.forward;
        }
        
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (grounded && Input.GetButtonDown("Jump"))
        {
            yvel.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        yvel.y += gravity * Time.deltaTime;
        control.Move(yvel * Time.deltaTime);
        
        control.Move( speed * Time.deltaTime * moveAmount);
    }
}
