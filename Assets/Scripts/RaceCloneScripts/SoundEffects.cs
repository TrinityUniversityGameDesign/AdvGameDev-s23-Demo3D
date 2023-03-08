using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffects : MonoBehaviour
{
    public AudioClip explosionSound;
    
    private AudioSource aud;
    private Globals global;

    // Start is called before the first frame update
    void Start()
    {
        global = GameObject.FindObjectOfType<Globals>();
        global.onExplode.AddListener(PlayExplosionSound);
        
        aud = GetComponent<AudioSource>();
    }

    void PlayExplosionSound()
    {
        aud.PlayOneShot(explosionSound);
    }
}
