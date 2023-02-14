using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPCamera : MonoBehaviour
{
  /*
  public enum ThirdPersonCameraType
  {
    Track,
    Follow,
    Follow_TrackRotation,
    Follow_IndependentRotation,
    TopDown
  }
  public ThirdPersonCameraType mThirdPersonCameraType = 
    ThirdPersonCameraType.Track;
  public Transform mPlayer;
  */
  public Transform target;
  
  public Vector3 positionOffset = new Vector3(0.0f, 2.0f, -2.5f);
  public Vector3 angleOffset = new Vector3(0.0f, 0.0f, 0.0f);
  public float damping = 5.0f;

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
  
  void CameraMove_Follow(bool allowRotationTracking = false)
  {
    // We apply the initial rotation to the camera.
    Quaternion initialRotation = Quaternion.Euler(angleOffset);

    // added the following code to allow rotation tracking of the player
    // so that our camera rotates when the player rotates and at the same
    // time maintain the initial rotation offset.
    if (allowRotationTracking)
    {
      Quaternion rot = Quaternion.Lerp(transform.rotation,
          target.rotation * initialRotation,
          Time.deltaTime * damping);

      transform.rotation = rot;
    }
    else
    {
      transform.rotation = Quaternion.RotateTowards(
        transform.rotation,
        initialRotation,
        damping * Time.deltaTime);
    }

    Vector3 desiredPosition = target.position + transform.rotation * positionOffset;
    
    // Finally, we change the position of the camera, 
    // not directly, but by applying Lerp.
    Vector3 position = Vector3.Lerp(transform.position,
        desiredPosition,
        Time.deltaTime * damping);

    transform.position = position;
  }

}