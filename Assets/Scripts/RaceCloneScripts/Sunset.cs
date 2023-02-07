using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunset : MonoBehaviour
{
    private Quaternion initQuaternion;
    private float angleX;
    public float speed = 4f;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.rotation.eulerAngles);
        initQuaternion = transform.rotation;
        angleX = initQuaternion.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        angleX += speed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(angleX, initQuaternion.eulerAngles.y, initQuaternion.eulerAngles.z);
    }

    void Reset()
    {
        transform.rotation = initQuaternion;
    }
}
