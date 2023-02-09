using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class GameUIController : MonoBehaviour
{
    private Globals global;

    private GameObject startScreenPanel;
    private GameObject endScreenPanel;
    private GameObject scoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        //Get the UI elements
        startScreenPanel = GameObject.Find("StartScreenPanel");
        endScreenPanel = GameObject.Find("EndScreenPanel");
        scoreText = GameObject.Find("ScoreText");
        
        //Set initial UI state
        startScreenPanel.SetActive(true);
        endScreenPanel.SetActive(false);
        scoreText.SetActive(false);
        
        //Get the event handler, and register events
        global = GetComponent<Globals>();
        global.onStart.AddListener(StartGame);
        global.onExplode.AddListener(EndGame);
        global.onSundown.AddListener(EndGame);
    }

    public void StartGame()
    {
        global.state = GameState.RUNNING;
        startScreenPanel.SetActive(false);
        endScreenPanel.SetActive(false);
        scoreText.SetActive(true);
    }
    
    public void EndGame()
    {
        global.state = GameState.ENDSCREEN;
        endScreenPanel.transform.Find("FinalScoreText").GetComponent<TMP_Text>().text = "Score: " + global.score;
        endScreenPanel.SetActive(true);
        scoreText.SetActive(false);
    }
    
    private void Update()
    {
        if (global.state == GameState.STARTSCREEN || global.state == GameState.ENDSCREEN)
        {
            if(Input.anyKeyDown)
            {
                global.onStart.Invoke();
            }
        }
        
        if (global.state == GameState.RUNNING)
        {
            scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + global.score;
        }
    }
}
