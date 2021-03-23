using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{

    private int rows = 5;
    private int cols = 9;

    private float tileSize = 1.3f;
    GameObject[] tiles;
    // Start is called before the first frame update
    void Start()
    {
        tiles = new GameObject[rows * cols];
        GenerateGrid();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateGrid()
    {
        GameObject refTile = (GameObject)Instantiate(Resources.Load("Prefabs/tileObject"));


        int index = 0;
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                GameObject tile = (GameObject)Instantiate(refTile, transform);
                float posx = c * tileSize;
                float posy = r * -tileSize;

                tile.transform.position = new Vector2(posx, posy);
                tile.name = ("Tile Row " + r + " , Col " + c);
                tile.GetComponent<SpriteRenderer>().sortingOrder = -1;
                tiles[index] = tile;
                index++;
            }

        }
        transform.position = new Vector3(1.9f, 5.31f, -1f);
        Destroy(refTile);
    }


    public GameObject CheckForMouseCollision(Vector3 mousePos)
    {

        for (int i = 0; i < tiles.Length; i++)
        {

            if (mousePos.x > tiles[i].transform.position.x - tileSize / 2 && mousePos.x < tiles[i].transform.position.x + tileSize / 2)
            {

                if (mousePos.y > tiles[i].transform.position.y - tileSize / 2 && mousePos.y < tiles[i].transform.position.y + tileSize / 2)
                {

                    return tiles[i];

                }
            }
        }

        // m.x > t.x - (size /2)
        return null;
    }
}
