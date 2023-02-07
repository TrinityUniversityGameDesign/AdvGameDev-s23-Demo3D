using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace RaceCloneScripts
{
    public class PlayerMovementForces : MonoBehaviour
    {
        
        public float maxForwardSpeed = 200f;
        public float forwardPower = 3f;
        public float turnPower = 5000f;

        public Transform sunlight;
        public LayerMask hitMask;
        
        private Rigidbody rb;

        private Vector3 forwardForce;
        private Vector3 turnForce;
        
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            forwardForce = transform.forward * forwardPower;
            turnForce = transform.right * turnPower;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 sunDir = -sunlight.forward;
            float horiz = Input.GetAxis("Horizontal");
            //Debug.Log(horiz);
            rb.AddForce(horiz * Time.deltaTime * turnForce);
            if (rb.velocity.z < maxForwardSpeed)
            {
                float lightAmt = Vector3.Dot(transform.up, sunDir);
                Debug.Log(lightAmt);
                rb.AddForce(forwardForce * lightAmt);
            }
            
            //Check if in shadow
            
            RaycastHit[] hits = Physics.RaycastAll(transform.position, sunDir, 100f, hitMask);
            if (hits.Length > 0)
            {
                if (rb.velocity.z > 0)
                {
                    rb.AddForce(-forwardForce * 10f);
                }
            }

        }
        
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                Debug.Log("Collision");
                //Destroy(gameObject);
                rb.velocity = Vector3.zero;
                forwardForce = Vector3.zero;
                forwardPower = 0f;
                turnForce = Vector3.zero;
                turnPower = 0f;
            }
        }
        
    }

}
