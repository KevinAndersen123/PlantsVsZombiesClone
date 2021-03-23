using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class Data
{
    public static string m_previousScene = "MainMenu";
    public static string m_currentScene = "MainMenu";
    public static bool levelOneComplete = false;
    public static bool levelTwoComplete = false;
    public static int levelOneZombiesLeft;
    public static int levelTwoZombiesLeft = 36;
    public static int levelThreeZombiesLeft = 48;
    public static int s_currentLevel = 1;
    public static float s_levelTime = 0;
    public static int s_peaShooterCounter = 0;
    public static int s_iceShooterCounter = 0;
    public static int s_cherryCounter = 0;
    public static int s_sunflowerCounter = 0;
    public static int s_wallnutCounter = 0;
    public static int s_shovelCounter = 0;
    public static int s_zombiesKilled = 0;
}

public class Manager : MonoBehaviour
{
    public void changeScreen(string t_scene)
    {
        Data.m_previousScene = Data.m_currentScene;
        Data.m_currentScene = t_scene;
        if (Data.m_currentScene == "MainMenu")
        {
            StartCoroutine(FindObjectOfType<AudioManager>().Crow());
        }
        if (Data.m_currentScene == "LevelOne")
        {
            ResetDataCounters();
            Data.s_currentLevel = 1;
            FindObjectOfType<AudioManager>().Stop("MainBG");
            FindObjectOfType<AudioManager>().Play("BG_Gameplay");
            //int amountOfZombies = GetComponent<WaveSpawner>().waves.Length;
            Debug.Log("First Level Zombies :");
            Debug.Log(Data.levelOneZombiesLeft);
            //Data.levelOneZombiesLeft
        }
        else if (Data.m_currentScene == "LevelTwo")
        {
            ResetDataCounters();
            Data.s_currentLevel = 2;
            FindObjectOfType<AudioManager>().Stop("MainBG");
            FindObjectOfType<AudioManager>().Play("BG_Gameplay");
        }
        else if (Data.m_currentScene == "LevelThree")
        {
            ResetDataCounters();
            Data.s_currentLevel = 3;
            FindObjectOfType<AudioManager>().Stop("MainBG");
            FindObjectOfType<AudioManager>().Play("BG_FinalWave");
        }
        else if (Data.m_currentScene == "Survival")
        {
            ResetDataCounters();
            Data.s_currentLevel = 4;
            FindObjectOfType<AudioManager>().Stop("MainBG");
            FindObjectOfType<AudioManager>().Play("BG_FinalWave");
        }
        else if (Data.m_currentScene == "Gameover")
        {
            FindObjectOfType<AnalyticsManager>().SendData(false);
            FindObjectOfType<AudioManager>().Stop("BG_Gameplay");
            FindObjectOfType<AudioManager>().Stop("BG_FinalWave");
            FindObjectOfType<AudioManager>().Play("Gameover");
        }
        else if (Data.m_currentScene == "Scoreboard")
        {
            FindObjectOfType<AnalyticsManager>().SendData(false);
            FindObjectOfType<AudioManager>().Stop("BG_Gameplay");
            FindObjectOfType<AudioManager>().Stop("BG_FinalWave");
            FindObjectOfType<AudioManager>().Play("Gameover");
        }
        else if (Data.m_currentScene == "Victory")
        {
            FindObjectOfType<AnalyticsManager>().SendData(true);
            FindObjectOfType<AudioManager>().Stop("BG_Gameplay");
            FindObjectOfType<AudioManager>().Stop("BG_FinalWave");
            FindObjectOfType<AudioManager>().Play("Victory");
        }
        else if (Data.m_currentScene == "LevelSelect")
        {
            FindObjectOfType<AudioManager>().Stop("BG_Gameplay");
            FindObjectOfType<AudioManager>().Stop("BG_FinalWave");
            if (Data.m_previousScene != "MainMenu")
                FindObjectOfType<AudioManager>().Play("MainBG");
        }
        SceneManager.LoadSceneAsync(t_scene);
    }

    //unlock level when it is complete
    public void CompletedLevel(int t_level)
    {
        switch (t_level)
        {
            case 1:
                Data.levelOneComplete = true;
                break;
            case 2:
                Data.levelTwoComplete = true;
                break;
            default:
                break;
        }
    }
    public void setAmountOfZombies(int t_amount)
    {
        if (Data.m_currentScene == "LevelOne")
        {
            Data.levelOneZombiesLeft = t_amount;
            Debug.Log("THIS IS THE AMOUNT : ");
            Debug.Log(t_amount);
        }
        else if (Data.m_currentScene == "LevelTwo")
        {
            Data.levelTwoZombiesLeft = t_amount;
            Debug.Log("THIS IS THE AMOUNT : ");
            Debug.Log(t_amount);
        }
        else if (Data.m_currentScene == "LevelThree")
        {
            Data.levelThreeZombiesLeft = t_amount;
            Debug.Log("THIS IS THE AMOUNT : ");
            Debug.Log(t_amount);
        }
    }

    private void Update()
    {
        if (Data.m_currentScene == "LevelOne" || Data.m_currentScene == "LevelTwo" || Data.m_currentScene == "LevelThree" || Data.m_currentScene == "Survival")
        {
            Data.s_levelTime += Time.deltaTime;
        }
    }

    private void ResetDataCounters()
    {
        Data.s_levelTime = 0;
        Data.s_peaShooterCounter = 0;
        Data.s_cherryCounter = 0;
        Data.s_sunflowerCounter = 0;
        Data.s_wallnutCounter = 0;
        Data.s_shovelCounter = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}


