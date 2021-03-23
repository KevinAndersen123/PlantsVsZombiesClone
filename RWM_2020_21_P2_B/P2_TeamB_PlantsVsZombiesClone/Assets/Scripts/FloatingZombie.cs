using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingZombie : MonoBehaviour
{

    bool alive = true;
    public float maxHealth;
    public float health = 10;

    public float floatSpeed = 2;

    bool goUp = false;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        aliveCheck();

        if (alive)
        {
            moveLeft();
            boundryCheck();
            moveUpDown();
        }
        else
        {
            killZombie();
            Destroy(gameObject);
        }
    }

    void moveLeft()
    {
        //Move left towards the house
        transform.Translate(Vector2.left * Time.deltaTime * floatSpeed);
    }

    void boundryCheck()
    {
        //Change bool for up on boundry
        if (transform.position.y < 0)
        {
            goUp = true;
        }
        else if (transform.position.y > 6)
        {
            goUp = false;
        }
    }

    void moveUpDown()
    {
        //Change translate depending on bool
        if (goUp)
        {
            transform.Translate(Vector2.up * Time.deltaTime * floatSpeed);
        }
        else
        {
            transform.Translate(Vector2.down * Time.deltaTime * floatSpeed);
        }
    }

    public void killZombie()
    {
        if (Data.m_currentScene == "LevelOne")
        {
            Data.levelOneZombiesLeft--;
        }
        else if (Data.m_currentScene == "LevelTwo")
        {
            Data.levelTwoZombiesLeft--;
        }
        else if (Data.m_currentScene == "LevelThree")
        {
            Data.levelThreeZombiesLeft--;
        }
        else if (Data.m_currentScene == "Survival")
        {

        }
    }

    private void aliveCheck()
    {
        if (health > 0)
        {
            alive = true;
        }
        else if(health < 0)
        {
            alive = false;
        }
    }

    public void OnCollisionStay2D(UnityEngine.Collision2D collision)
    {
        if(collision.gameObject.name == "Wallnut(Clone)")
        {
            health -= 100;
        }
        else if (collision.gameObject.tag == "Plant")
        {
            collision.gameObject.GetComponent<ItemObjectScript>().takeDamage(1000);
            health -= 100;
        }
        else if(collision.gameObject.tag == "Seed")
        {
            health -= 100;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ////Change
        //if (collision.gameObject.tag == "Seed")
        //{
        //    health -= 100;
        //}

    }
}
