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
    positionOffset = transform.position - target.position;
    angleOffset = transform.eulerAngles - target.eulerAngles;
    Cursor.lockState = CursorLockMode.Locked;
  }

  void LateUpdate()
  { 
    //CameraMove_Track();
    //CameraMove_Follow();
    CameraMove_Follow(true);
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

}