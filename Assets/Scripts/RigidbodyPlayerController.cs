using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 100f;

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        transform.position += transform.forward * moveZ * moveSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, moveX * rotateSpeed * Time.deltaTime);
    }
}