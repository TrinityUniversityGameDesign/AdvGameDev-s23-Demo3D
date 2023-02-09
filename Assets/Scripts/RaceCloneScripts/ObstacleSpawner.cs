using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RaceCloneScripts
{
    public class ObstacleSpawner : MonoBehaviour
    {
        public GameObject obstaclePrefab;
        public int numObstacles = 1000;
        
        public float width = 1000f;
        public float length = 1000f;
        
        private GameObject[] obstacles;
        private Globals global;
        
        // Start is called before the first frame update
        void Start()
        {
            global = GameObject.FindObjectOfType<Globals>();
            global.onStart.AddListener(InitialSpawn);
        }
        
        void InitialSpawn()
        {
            obstacles = new GameObject[numObstacles];
            for (int i = 0; i < numObstacles; i++)
            {
                GameObject tmp = Instantiate(obstaclePrefab) as GameObject;
                tmp.transform.position = new Vector3(Random.Range(-width / 2, width / 2), tmp.transform.position.y,
                    Random.Range(0, length));
                tmp.transform.SetParent(transform);
                obstacles[i] = tmp;
            }    
        }
        
    }
    
}
