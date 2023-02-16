using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPCamera : MonoBehaviour
{
  public Transform target;
  public float damping = 5.0f;
  
  private Vector3 positionOffset;
  private Vector3 angleOffset;
  
  void Start()
  {
    Vector3 x = new Vector3(1, 0, 0);
    Quaternion q = Quaternion.Euler(0,90,0);
    Debug.Log(x);
    Debug.Log(q);
    Vector3 xRotQ = q * x;
    Debug.Log(xRotQ);
    
    positionOffset = transform.position - target.position;
    angleOffset = transform.eulerAngles - target.eulerAngles;
    Cursor.lockState = CursorLockMode.Locked;
  }

  void LateUpdate()
  { 
    //CameraMove_Track();
    //CameraMove_Follow();
    CameraMove_Follow(true);
    //CameraMove_IndependentRotation();
  }

  void CameraMove_Track()
  {
    Vector3 targetPos = target.transform.position;
    targetPos.y += positionOffset.y;
    transform.LookAt(targetPos);
  }
  
  void CameraMove_Follow(bool trackRotation = false)
  {
    Quaternion initialRotation = Quaternion.Euler(angleOffset);
    if (trackRotation)
    {
      Quaternion rot = Quaternion.Lerp(transform.rotation, target.rotation * initialRotation, Time.deltaTime * damping);
      transform.rotation = rot;
    }
    else
    {
      transform.rotation = Quaternion.RotateTowards(transform.rotation, initialRotation, damping * Time.deltaTime);
    }

    Vector3 desiredPosition = target.position + transform.rotation * positionOffset;
    Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
    transform.position = position;
  }

  private float angleX = 0f;
  public float rotationSpeed = 5f;
  void CameraMove_IndependentRotation()
  {
    float mx, my;
    mx = Input.GetAxis("Mouse X");
    my = Input.GetAxis("Mouse Y");

    // We apply the initial rotation to the camera.
    Quaternion initialRotation = Quaternion.Euler(angleOffset);

    Vector3 eu = transform.rotation.eulerAngles;

    angleX -= my * rotationSpeed;

    // We clamp the angle along the X axis to be between 
    // the min and max pitch.
    angleX = Mathf.Clamp(angleX, -30f,30f);

    eu.y += mx * rotationSpeed;
    Quaternion newRot = Quaternion.Euler(angleX, eu.y, 0.0f) * initialRotation;

    transform.rotation = newRot;

    Vector3 desiredPosition = target.position + transform.rotation * positionOffset;
    Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
    transform.position = position;
  }

}