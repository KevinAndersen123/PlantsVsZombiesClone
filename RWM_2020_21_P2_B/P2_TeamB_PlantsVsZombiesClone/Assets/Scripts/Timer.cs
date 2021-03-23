using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    public static Timer timer;
    public Text timeText;
    private TimeSpan timePlaying;
    bool timeBegan = false;

    private float elapsedTime;


    // Start is called before the first frame update
    void Start()
    {
        timeText.text = "Time: 00:00:00";
        timeBegan = false;

        startTimer();
    }

    public void startTimer()
    {
        timeBegan = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }
    

    // Update is called once per frame
    void Update()
    {
        //timePlaying.TotalSeconds = Data.s_levelTime;
    }

    private IEnumerator UpdateTimer()
    {
        //If timer has started
        while(timeBegan)
        {
            //Add Time to Elapsed Time
            elapsedTime += Time.deltaTime;
            //Converts to time span instance
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            //Display and formatting
            string timePlayingStr = "Time: " + timePlaying.ToString("mm' : 'ss' : 'ff");
            //Assign to UI text
            timeText.text = timePlayingStr;

            yield return null;
        }
    }

}
