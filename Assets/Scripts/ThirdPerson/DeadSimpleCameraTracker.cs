using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSimpleCameraTracker : MonoBehaviour
{
    public Transform target;
    public float posDamping = 2f;
    public float rotDamping = 2f;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, posDamping * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, rotDamping * Time.deltaTime);
    }
}
