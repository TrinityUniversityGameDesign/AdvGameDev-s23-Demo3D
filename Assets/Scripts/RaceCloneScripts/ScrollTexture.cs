using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaceCloneScripts
{
    public class ScrollTexture : MonoBehaviour
    {
        private Material mat;
        private Vector3 lastPosition;
        private Vector2 texScale;
        public float scrollSpeed = 0.5f;
        
        // Start is called before the first frame update
        void Start()
        {
            mat = GetComponent<Renderer>().material;
            lastPosition = transform.position;
            texScale = mat.GetTextureScale("_MainTex");
            Debug.Log(texScale);
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 oldOffset = mat.GetTextureOffset("_MainTex");
            float deltaZ = lastPosition.z - transform.position.z;
            float deltaX = lastPosition.x - transform.position.x;
            Vector2 newOffset = new Vector2(oldOffset.x - deltaX / (transform.localScale.x / texScale.x),
                                            oldOffset.y - deltaZ / (transform.localScale.y / texScale.y));
            mat.SetTextureOffset("_MainTex", newOffset);
            
            lastPosition = transform.position;
        }
    }
}
