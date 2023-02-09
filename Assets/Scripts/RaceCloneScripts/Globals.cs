using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    STARTSCREEN,
    RUNNING,
    ENDSCREEN
};

public class Globals : MonoBehaviour
{
    public GameState state = GameState.STARTSCREEN;

    public UnityEvent onStart = new UnityEvent();
    public UnityEvent onExplode = new UnityEvent();
    public UnityEvent onSundown = new UnityEvent();

    public int score = 0;

    void Start()
    {
        onStart.AddListener(GlobalStart);
        onExplode.AddListener(GlobalExplode);
        onSundown.AddListener(GlobalSundown);
    }
    
    public void GlobalStart()
    {
        Debug.Log("onStart Invoked");
        score = 0;
    }
    public void GlobalExplode()
    {
        Debug.Log("onExplode Invoked");
    }
    public void GlobalSundown()
    {
        Debug.Log("onSundown Invoked");
        state = GameState.ENDSCREEN;
        onExplode.Invoke();
    }
    
}
