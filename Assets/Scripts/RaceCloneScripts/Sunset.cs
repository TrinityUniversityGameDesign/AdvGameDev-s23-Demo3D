using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunset : MonoBehaviour
{
    private Quaternion initQuaternion;
    private float angleX;
    public float speed = 4f;

    private Globals global;
    
    // Start is called before the first frame update
    void Start()
    {
        initQuaternion = transform.rotation;
        Reset();
        
        global = GameObject.FindObjectOfType<Globals>();
        global.onStart.AddListener(Reset);
    }

    // Update is called once per frame
    void Update()
    {
        if (global.state == GameState.RUNNING)
        {
            angleX += speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(angleX, initQuaternion.eulerAngles.y, initQuaternion.eulerAngles.z);

            if (angleX >= 180)
            {
                global.onSundown.Invoke();
            }
        }
    }

    public void Reset()
    {
        transform.rotation = initQuaternion;
        angleX = initQuaternion.eulerAngles.x;
    }
}
