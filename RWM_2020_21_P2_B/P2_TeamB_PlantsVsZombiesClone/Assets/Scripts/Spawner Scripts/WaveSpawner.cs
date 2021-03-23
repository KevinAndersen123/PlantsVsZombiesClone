using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    //Wave Timer
    public float timeBetweenWaves = 10;
    float waveTimer;
    
    //Waves
    public Wave[] waves;
    int currentWave = 0;

    //Poisitions
    public Vector3[] spawnPositions;
    int currentSpawnPos = 0;

    //Progress Bar
    //public ProgressBar progressBar;
    public ProgressBar pb;
    private float totalTime;
    float oneSecondAsPercentage;

    //Heavy Spawn indicator Varibles
    float widthProgressBar;
    float heightProgressBar;

    public bool start;

    void Start()
    {
        start = true;
        //Timer Begins at time between waves
        waveTimer = timeBetweenWaves;

        //Get total Time
        totalTime = waves.Length * timeBetweenWaves;

        //Get one second as a Percentage
         oneSecondAsPercentage = 100 / totalTime;

        //Assign width of progress bar
        widthProgressBar = pb.transform.GetComponent<RectTransform>().sizeDelta.x;
        //Assign Height of progress bar
        heightProgressBar = pb.transform.GetComponent<RectTransform>().sizeDelta.y;

        setUpFlags();

        //Update Progress Bar
        StartCoroutine(updateProgressBar());

        int amountOfZombies = 0;

        for (int i = 0; i < waves.Length; i++)
        {
            amountOfZombies += waves[i].amountOfObjects;
        }
        
        FindObjectOfType<Manager>().setAmountOfZombies(amountOfZombies);
    }

    void Update()
    {
      

        //Spawn Wave after Timer reaches zero
        if (waveTimer <= 0)
        {
            //Spawn the wave and reset time
            spawnWave();
            resetTimer();
        }
        //If Wave Timer is above zero take away time
        else if (waveTimer > 0)
        {
            //Take away deltaTime to countdown Wave Timer
            waveTimer -= Time.deltaTime;
        }
        if(Data.levelOneZombiesLeft == 0 && Data.m_currentScene == "LevelOne" && currentWave == waves.Length)
        {
            if (!Data.levelOneComplete)
            {
                FindObjectOfType<Manager>().CompletedLevel(1);
            }
            FindObjectOfType<LevelChanger>().FadeToLevel("Victory");
        }
        if (Data.levelTwoZombiesLeft == 0 && Data.m_currentScene == "LevelTwo" && currentWave == waves.Length)
        {
            if (!Data.levelTwoComplete)
            {
                FindObjectOfType<Manager>().CompletedLevel(2);
            }
            FindObjectOfType<LevelChanger>().FadeToLevel("Victory");
        }
        if (Data.levelThreeZombiesLeft == 0 && Data.m_currentScene == "LevelThree" && currentWave == waves.Length)
        {
            FindObjectOfType<LevelChanger>().FadeToLevel("Victory");
        }
    }

    void spawnWave()
    {
        //Check current wave isnt the last
        if(currentWave < waves.Length)
        {
            if (currentWave == 0)
            {
                FindObjectOfType<AudioManager>().Play("IncomingZombies");
            }
            //Spawn Amount of objects that are in the current Wave
            for (int i = 0; i < waves[currentWave].amountOfObjects; i++ )
            {
                
                spawnGameObject(waves[currentWave].gameObject);
            }

            //Increase Current Wave
            currentWave++;
        }
        else
        {

        }
    }

    void spawnGameObject(GameObject gameObject)
    {
        if (currentSpawnPos < spawnPositions.Length - 1)
        {
              currentSpawnPos++;
        }
        else
        {
            currentSpawnPos = 0;
        }

        //instantiate the gameObject
        Instantiate(gameObject, spawnPositions[currentSpawnPos], Quaternion.identity);
    }

    void resetTimer()
    {
        //Reset Timer
        waveTimer = timeBetweenWaves;
    }

    IEnumerator updateProgressBar()
    {
        while(true)
        {
            //Update the bar every second
            yield return new WaitForSeconds(1f);
            //progressBar.increaseProgress(oneSecondAsPercentage);
            pb.increaseProgress(oneSecondAsPercentage);
        }
    }

    public void setUpFlags()
    {
        for (int i = 0; i < waves.Length; i++)
        {
           // Debug.Log(waves.Length);
            if (waves[i].heavyWave)
            {
                // Debug.Log(widthProgressBar);

                //float xPos = pb.transform.position.x + ((widthProgressBar / waves.Length) * (i + 1));// (pb.transform.position.x + (-widthProgressBar / 2));
                //float yPos = pb.transform.position.y + (heightProgressBar) + 50;

                float xPos = -((widthProgressBar / waves.Length) * (i + 1)) +50;// (pb.transform.position.x + (-widthProgressBar / 2));
                float yPos = (heightProgressBar) + heightProgressBar/4;
                Debug.Log(pb.transform.position);

                Debug.Log(yPos);

                pb.placeHeavyWaveMarker(xPos, yPos);

            }
        }
        //Debug.Log("INSIDE");
        start = false;
    }
}
