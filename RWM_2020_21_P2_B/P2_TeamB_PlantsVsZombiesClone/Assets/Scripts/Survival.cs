using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survival : MonoBehaviour
{
    public GameObject normalZombie;
    public GameObject hardHatZombie;
    public GameObject flagZombie;
    public GameObject floatingZombie;

    private Wave[] waves;

    //Wave Timer
    float initialTimer = 0;
    float initialSetUpTime = 15;

    public float timeBetweenWaves;
    float waveTimer;

    //Waves
    int currentWave = 0;

    //Poisitions
    public Vector3[] spawnPositions;
    int currentSpawnPos = 0;

    // Start is called before the first frame update
    void Start()
    {
        Data.s_zombiesKilled = 0;
        //Random starting between waves
        //timeBetweenWaves = Random.Range(1, 5);
        timeBetweenWaves = 25;
    }

    void start()
    {
        //Timer Begins at time between waves
        waveTimer = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        if(initialTimer < initialSetUpTime )
        {
            initialTimer += Time.deltaTime;
        }
        else
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
        }

    }

    void spawnWave()
    {
        
        //Create New Wave
        Wave newWave = new Wave();

        //Assign name
        newWave.waveName = "New Wave";
        //Assign amount of zombies in the wave
        newWave.amountOfObjects = Random.Range(1, 5);

        //Random Zombie
        int gameObjectSelector = Random.Range(1, 4);

        if(newWave.amountOfObjects == 1)
        {
            newWave.gameObject = floatingZombie;
        }
        else if (gameObjectSelector == 1)
        {
            //Normal Zombie
            newWave.gameObject = normalZombie;
        }
        else if (gameObjectSelector == 2)
        {
            //Hardhat Zombie
            newWave.gameObject = hardHatZombie;
        }
        else if (gameObjectSelector == 3)
        {
            //Flag Zombie
            newWave.gameObject = flagZombie;
        }


        for (int i = 0; i < newWave.amountOfObjects; i++)
        {
     
        }

        for (int i = 0; i < newWave.amountOfObjects; i++)
        {
            Vector3 spawnVec = positionGenerator();

            //instantiate the gameObject
            Instantiate(newWave.gameObject,spawnVec, Quaternion.identity);
        }

        //Increase Current Wave
        currentWave++;

        if(timeBetweenWaves >= 2)
        {
            timeBetweenWaves -= 1;
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

    Vector3 positionGenerator()
    { 
        float xVal = 0;
        float yVal = 0;

        //Y Generator
        float randGenY = Random.Range(1, 6);
        //Y Assignment
        if (randGenY == 1)
        {
            yVal = 0.4f;
        }
        else if (randGenY == 2)
        {
            yVal = 1.7f;
        }
        else if(randGenY == 3)
        {
            yVal = 3.0f;
        }
        else if (randGenY == 4)
        {
            yVal = 4.3f;
        }
        else if(randGenY == 5)
        {
            yVal = 5.6f;
        }

        //X Ganerator
        float randGenX = Random.Range(1, 6);
        if (randGenX == 1)
        {
            xVal = 15.4f;
        }
        else if (randGenX == 2)
        {
            xVal = 16.0f;
        }
        else if (randGenX == 3)
        {
            xVal = 17.0f;
        }
        else if (randGenX == 4)
        {
            xVal = 17.5f;
        }
        else if (randGenX == 5)
        {
            xVal = 18.0f;
        }

        Vector3 returnVec = new Vector3(xVal, yVal,0);

        return returnVec;
    }

}
