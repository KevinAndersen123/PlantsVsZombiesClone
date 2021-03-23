using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunDrop : MonoBehaviour
{
    public int moveSpeed = 1;

    void Start()
    {

     
    }

    void Update()
    {
        //Move Down at consistent speed
        transform.Translate(Vector2.down * Time.deltaTime * moveSpeed);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject.Find("Shop Prefab Variant").GetComponent<ShopMenuController>().UpdatePlayerMoney(50);

            //Destroy the sun Drop
            Destroy(gameObject);
        }
    }
}
