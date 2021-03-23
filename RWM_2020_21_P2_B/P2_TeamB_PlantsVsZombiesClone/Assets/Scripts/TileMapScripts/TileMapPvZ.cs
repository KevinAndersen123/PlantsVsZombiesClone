using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TileMapPvZ : MonoBehaviour
{
    private MyTileMap tileMap;
    private int gridWidth;
    private int gridHeight;
    private float cellSize;
    private Vector3 mapOriginPosition;
    private Sprite initialTileSpriteDark;
    private Sprite initialTileSpriteLight;
    private Sprite background;
    private GameObject[] lawnMowerArrayGameObj;

    // Start is called before the first frame update
    void Start()
    {
        background = Resources.Load<Sprite>("TileSprites/houseBackgroundDirt");
        this.gameObject.GetComponent<SpriteRenderer>().sprite = background;
        this.gameObject.transform.position = new Vector3(6f, 3.25f, 0);
        this.gameObject.transform.localScale = new Vector3(0.6f, 0.54f, 1);
       
        gridWidth = 9;
        gridHeight = 5;
        cellSize = 1.3f;
        mapOriginPosition = new Vector3(1.9f, 0.1f, 0);  // set origin

        initialTileSpriteDark = Resources.Load<Sprite>("TileSprites/grassTileDark");
        initialTileSpriteLight = Resources.Load<Sprite>("TileSprites/grassTileLight");
        tileMap = new MyTileMap(gridWidth, gridHeight, cellSize, mapOriginPosition, initialTileSpriteDark, initialTileSpriteLight, this.gameObject.transform);
        Camera.main.transform.position = new Vector3((gridWidth * cellSize) / 2, (gridHeight * cellSize) / 2, -10);

        //lawnmowers
        lawnMowerArrayGameObj = new GameObject[gridHeight];
        Vector3 startPos = mapOriginPosition;
        startPos.y += (cellSize / 2); // move up to middle of tile
        startPos.x -= cellSize / 2; // move left so off the grid map
        Vector3 startPosOffset = new Vector3(-.6f, -.65f, 0);
        for (int i = 0; i < lawnMowerArrayGameObj.Length; i++)
        {
            lawnMowerArrayGameObj[i] = new GameObject(); //startPos + startPosOffset
            lawnMowerArrayGameObj[i].AddComponent<LawnmowerBehaviour>();
            lawnMowerArrayGameObj[i].GetComponent<LawnmowerBehaviour>().SetUp(startPos + startPosOffset);
            startPosOffset.y += cellSize;
        }
    }
}
