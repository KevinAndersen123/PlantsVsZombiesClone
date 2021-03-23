using UnityEngine;
using System.Collections;

public class MyTileMap : MonoBehaviour
{
    private MyGrid<GameObject> grid;
    public static int[,] mapSpriteIndex = new int[10,5];
    public Sprite[] spriteArray;
    static bool isCustomMap = new bool();
    private int width;
    private int height;

    public MyTileMap() { }
    // Overloaded constructor for setting all sprites
    public MyTileMap(int width, int height, float cellSize, Vector3 originPosition, Sprite t_spriteOne, Sprite t_spriteTwo, Transform parent)
    {
        this.width = width;
        this.height = height;
        spriteArray = Resources.LoadAll<Sprite>("Sprites/");// load sprite assets
        grid = new MyGrid<GameObject>(width, height, cellSize, originPosition);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid.gridArray[x, y] = new GameObject("TileObject");
                grid.gridArray[x, y].AddComponent<TileMapObject>();
                //grid.gridArray[x, y].transform.SetParent(parent);
                grid.gridArray[x, y].GetComponent<TileMapObject>().init(cellSize, originPosition);
                grid.gridArray[x, y].GetComponent<TileMapObject>().SetXY(x, y);
                if ((x + y) % 2 == 0)
                {
                    grid.gridArray[x, y].GetComponent<TileMapObject>().SetTileMapSprite(t_spriteOne);
                }
                else
                {
                    grid.gridArray[x, y].GetComponent<TileMapObject>().SetTileMapSprite(t_spriteTwo);
                }

            }
        }
        if (isCustomMap)
        {
            setInitialSaveMapValues(); // sets sprites for tiles
            setUpSavedMap();
        }
    }
    public MyTileMap(int width, int height, float cellSize, Vector3 originPosition)
    {
        spriteArray = Resources.LoadAll<Sprite>("Sprites/");// load sprite assets
        grid = new MyGrid<GameObject>(width, height, cellSize, originPosition);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid.gridArray[x, y].AddComponent<TileMapObject>();// = new TileMapObject(x, y, cellSize);
                grid.gridArray[x, y].transform.SetParent(this.transform);
                grid.gridArray[x, y].GetComponent<TileMapObject>().init(cellSize, originPosition);
                grid.gridArray[x, y].GetComponent<TileMapObject>().SetXY(x, y);
            }
        }
        mapSpriteIndex = new int[width, height];
    }
    // Overloaded constructor for setting all sprites
    public MyTileMap(int width, int height, float cellSize, Vector3 originPosition, Sprite t_sprite)
    {
        spriteArray = Resources.LoadAll<Sprite>("Sprites/");// load sprite assets
        grid = new MyGrid<GameObject>(width, height, cellSize, originPosition);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid.gridArray[x, y].AddComponent<TileMapObject>();// = new TileMapObject(x, y, cellSize);
                grid.gridArray[x, y].transform.SetParent(this.transform);
                grid.gridArray[x, y].GetComponent<TileMapObject>().init(cellSize, originPosition);
                grid.gridArray[x, y].GetComponent<TileMapObject>().SetXY(x, y);
                grid.gridArray[x, y].GetComponent<TileMapObject>().SetTileMapSprite(t_sprite);
            }
        }
        mapSpriteIndex = new int[width, height];
        if (isCustomMap)
        {
            setUpSavedMap();
        }
    }

    public void setMapSpriteIndex(int x, int y, int value)
    {
        mapSpriteIndex[x, y] = value;
    }

    public void setInitialSaveMapValues()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if ((x + y) % 2 == 0)
                {
                    grid.gridArray[x, y].GetComponent<TileMapObject>().SetTileMapSprite(spriteArray[0]);
                }
                else
                {
                    grid.gridArray[x, y].GetComponent<TileMapObject>().SetTileMapSprite(spriteArray[2]);
                    if (mapSpriteIndex[x, y] == 0)
                        mapSpriteIndex[x, y] = 26;
                }
            }
        }

    }
    public void setUpSavedMap()
    { 
        for (int x = 0; x < grid.GetWorldWidth(); x++)
        {
            for (int y = 0; y < grid.GetWorldHeight(); y++)
            {
                grid.gridArray[x, y].GetComponent<TileMapObject>().SetTileMapSprite(spriteArray[mapSpriteIndex[x, y]]);
            }
        }
    }
    public void useCustomMap()
    {
        isCustomMap = true;
    }
}
