using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float maxHealth;
    public float walkSpeed;
    float health;
    bool attackingPlant = false;
    private int moans = 1;
    private int bite = 1;
    private bool isFrozen = false;
    private float freezeTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayMoans());
        //Zombie Starts with max health
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFrozen)
        {
            freezeTime += Time.deltaTime;
            if (freezeTime > 2)
            {
                isFrozen = false;
                this.GetComponent<Animator>().enabled = true; // freeze ended, start animation
                this.GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePositionX;
            }
        }
        else
        {
            if (attackingPlant)
            {

            }
            else if (GetComponent<Animator>().GetBool("isDead") == false)
            {
                //Keep walking towards house if arent attacking
                walk();
            }
        }
        
    }

    void walk()
    {
        //Move left towards the house
        transform.Translate(Vector2.left * Time.deltaTime * walkSpeed);
    }

    public void killZombie()
    {
        GetComponent<Animator>().SetBool("isDead", true);

        StartCoroutine("WaitToDestroy");
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
        else if(Data.m_currentScene == "Survival")
        {
            //Zombie death counter here
            Data.s_zombiesKilled += 1;
        }
    }
    public void OnCollisionStay2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.tag == "Plant")
        {
            attackingPlant = true;
            FindObjectOfType<AudioManager>().Play("ZombieBite1");
            collision.gameObject.GetComponent<ItemObjectScript>().takeDamage(1);
        }
    }

    public void OnCollisionExit2D(UnityEngine.Collision2D collision)
    {
        //Check if exiting plant
        if (collision.gameObject.tag == "Plant")
        {
            //No longer attacking plant
            attackingPlant = false;
        }

    }
    private IEnumerator PlayMoans()
    {
        yield return new WaitForSeconds(4.0f);
        if(GetComponent<Animator>().GetBool("isDead") == false)
        {
            FindObjectOfType<AudioManager>().Play("Zombie2");
            StartCoroutine(PlayMoans());
        }
    }
    void decreaseHealth(float damage)
    {
        //Check for heal equal to zero
        if (health >= 0)
        {
            health -= damage;
        }
        else
            killZombie();
    }

    IEnumerator WaitToDestroy()
    {
        // suspend execution for 1 seconds
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Seed")
        {
            if (GetComponent<Animator>().GetBool("isDead") == false)
            {
                decreaseHealth(45f);
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.tag == "IceSeed")
        {
            if (GetComponent<Animator>().GetBool("isDead") == false)
            {
                decreaseHealth(45f);
                Destroy(collision.gameObject);
                // freeze zombie
                isFrozen = true;
                freezeTime = 0;
                this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                this.GetComponent<Animator>().enabled = false; // freeze/stop animation
            }
        }
       
    }

}
