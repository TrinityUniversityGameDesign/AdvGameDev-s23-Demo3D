using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioClip endMusic;
    
    private AudioSource aud;
    private Globals global;

    // Start is called before the first frame update
    void Start()
    {
        global = GameObject.FindObjectOfType<Globals>();
        global.onStart.AddListener(SwitchToGameMusic);
        global.onSundown.AddListener(SwitchToEndMusic);
        global.onExplode.AddListener(SwitchToEndMusic);
        
        aud = GetComponent<AudioSource>();
        aud.loop = true;
        aud.clip = menuMusic;
        aud.Play();
    }

    void SwitchToGameMusic()
    {
        aud.clip = gameMusic;
        aud.Play();
    }
    
    void SwitchToEndMusic()
    {
        aud.clip = endMusic;
        aud.Play();
    }
    
    
}
