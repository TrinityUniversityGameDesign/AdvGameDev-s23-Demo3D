using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseControl : MonoBehaviour
{
    public GameObject target; // the object to focus on
    public float distance = 10.0f; // distance between camera and target
    public float xSpeed = 250.0f; // speed of horizontal rotation
    public float ySpeed = 120.0f; // speed of vertical rotation
    public float yMinLimit = -20; // minimum vertical angle
    public float yMaxLimit = 80; // maximum vertical angle
    public float distanceMin = .5f; // minimum distance
    public float distanceMax = 15f; // maximum distance
    float x = 0.0f; // horizontal angle
    float y = 0.0f; // vertical angle

    // Use this for initialization
    void Start ()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }
    void LateUpdate ()
    {
        if (target)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
            y = ClampAngle(y, yMinLimit, yMaxLimit);
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel")*5, distanceMin, distanceMax);
            RaycastHit hit;
            if (Physics.Linecast (target.transform.position, transform.position, out hit)) {
                distance -=  hit.distance;
            }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.transform.position;
            transform.rotation = rotation;
            transform.position = position;
        }
    }
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
