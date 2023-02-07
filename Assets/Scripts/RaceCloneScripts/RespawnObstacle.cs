using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RaceCloneScripts
{
    public class RespawnObstacle : MonoBehaviour
    {
        private ObstacleSpawner spawner;
        
        // Start is called before the first frame update
        void Start()
        {
            spawner = GameObject.Find("Obstacles").GetComponent<ObstacleSpawner>();   
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Culling"))
            {
                Vector3 newLoc = new Vector3(
                    Random.Range(Camera.main.transform.position.x - spawner.width / 2,
                                   Camera.main.transform.position.x + spawner.width / 2),
                    transform.position.y,
                    spawner.length / 2 + Camera.main.transform.position.z);
                transform.position = newLoc;
            }
        }
    }
}

