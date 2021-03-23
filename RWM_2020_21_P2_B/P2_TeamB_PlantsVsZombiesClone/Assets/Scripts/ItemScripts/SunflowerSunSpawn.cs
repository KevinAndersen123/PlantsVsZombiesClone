using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunflowerSunSpawn : MonoBehaviour
{

    public float moveSpeed = 0.2f;
    int timeToLive;
    void Start()
    {
        timeToLive = 800;

    }

    void Update()
    {
        //Move Down at consistent speed
        if (timeToLive > 0)
        {
            transform.Translate(Vector2.down * Time.deltaTime * moveSpeed);
        }
        else
        {
            Destroy(gameObject);
        }
        timeToLive--;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FindObjectOfType<AudioManager>().Play("Select");
            GameObject.Find("Shop Prefab Variant").GetComponent<ShopMenuController>().UpdatePlayerMoney(50);

            //Destroy the sun Drop
            Destroy(gameObject);
        }
    }
}
