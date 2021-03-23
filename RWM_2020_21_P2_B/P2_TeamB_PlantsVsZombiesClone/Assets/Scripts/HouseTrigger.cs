using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Zombie")
        {
            if (Data.m_currentScene == "Survival")
            {
                FindObjectOfType<LevelChanger>().FadeToLevel("Scoreboard");
            }
            else
            {
                Debug.Log("Zombie Detected");
                FindObjectOfType<LevelChanger>().FadeToLevel("Gameover");
            }
        }
    }
}
