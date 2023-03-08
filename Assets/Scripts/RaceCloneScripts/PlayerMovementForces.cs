using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

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
        
        private Globals global;

        private Image speedImage;
        
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            forwardForce = transform.forward * forwardPower;
            turnForce = transform.right * turnPower;

            speedImage = GetComponentInChildren<Image>();

            //Register functions with events
            global = GameObject.Find("GameController").GetComponent<Globals>();
            global.onStart.AddListener(ResetPlayer);
            
        }
        
        public void ResetPlayer()
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            speedImage.rectTransform.sizeDelta = new Vector2(0f, 0.25f);
        }

        // Update is called once per frame
        void Update()
        {
            if (global.state == GameState.RUNNING)
            {
                Vector3 sunDir = -sunlight.forward;
                float horiz = Input.GetAxis("Horizontal");
                rb.AddForce(horiz * Time.deltaTime * turnForce);
                if (rb.velocity.z < maxForwardSpeed)
                {
                    float lightAmt = Vector3.Dot(transform.up, sunDir);
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

                global.score += (int)rb.velocity.z;
            }
            
            //Update the speed indicator
            float speedFrac = rb.velocity.z / maxForwardSpeed;
            speedImage.rectTransform.sizeDelta = new Vector2(speedFrac * 2f, 0.25f);

        }
        
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                if (global.state == GameState.RUNNING)
                {
                    global.onExplode.Invoke();
                }
            }
        }
        
    }

}
