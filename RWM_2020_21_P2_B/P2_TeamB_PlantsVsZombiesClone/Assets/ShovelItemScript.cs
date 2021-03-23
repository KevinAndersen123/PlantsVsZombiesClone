using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelItemScript : MonoBehaviour
{

    bool isSelected = false;

    // Update is called once per frame
    void Update()
    {
        GetComponent<DragItem>().MoveItem();

        GameObject grid = GameObject.FindGameObjectWithTag("ItemGrid");

        if (Input.GetMouseButtonDown(0))
        {
            GameObject tile;

            tile = grid.GetComponent<ItemGrid>().CheckForMouseCollision(transform.position);
            if (tile != null) // and check if mouse position is over an empty tile when implemented
            {
                if (tile.transform.childCount != 0)
                {
                    for (int i = 0; i < tile.transform.childCount; i++)
                    {
                        Data.s_shovelCounter++;
                        Transform child = tile.transform.GetChild(i);
                        Destroy(child.transform.gameObject);
                    }                
                    Destroy(this.gameObject);
                }
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public bool GetSelected() { return isSelected; }
    public void ChangeSelected(bool sel) { isSelected = sel; }

}
