using UnityEngine;
using System.Collections;
// Author: Josh Browne
public class MyTileMapEditor : MonoBehaviour
{
    public MyTileMapEditor()
    {
    }
    public void init(int width, int height, float cellSize, Vector3 originPosition, float textureSelectPanelOffset)
    {
        originPosition = new Vector3((width * cellSize) / 2 + textureSelectPanelOffset, (height * cellSize) / 2, -10);
        originPosition = Camera.main.transform.position - originPosition;

        spriteSelection = this.gameObject.GetComponent<SpriteSelection>();
        spriteSelection.init(originPosition.x + (width * cellSize), height * cellSize, cellSize, height * cellSize, cellSize);
        currentSprite = spriteSelection.spriteArray[0];
        currentSprite.rect.size.Set(cellSize, cellSize);
        grid = new MyGrid<GameObject>();
        grid.init(width, height, cellSize, originPosition);
        cellsize = cellSize;
        originposition = originPosition;
        // For grid lines
        LineRenderer lineRenderer = this.gameObject.AddComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 241;
        lineRenderer.SetWidth(0.05f, 0.05f);
        lineRenderer.material.color = Color.white;
        lineRenderer.startColor = Color.grey;
        lineRenderer.endColor = Color.grey;
        // Draw grid lines
        int count = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid.gridArray[x, y] = new GameObject("TileGameObject");
                grid.gridArray[x, y].transform.SetParent(this.gameObject.transform);
                grid.gridArray[x, y].AddComponent<TileMapObject>();
                grid.gridArray[x, y].GetComponent<TileMapObject>().init(cellSize, originPosition);
                grid.gridArray[x, y].GetComponent<TileMapObject>().SetXY(x + originPosition.x, y + originPosition.y);
                //line stuff..
                lineRenderer.SetPosition(count, GetWorldPosition(x, y));
                count++;
                lineRenderer.SetPosition(count, GetWorldPosition(x, y + 1));
                count++;
                lineRenderer.SetPosition(count, GetWorldPosition(x + 1, y + 1));
                count++;
                lineRenderer.SetPosition(count, GetWorldPosition(x + 1, y));
                count++;
                if (y < height - 1 || x == width - 1)
                {
                    lineRenderer.SetPosition(count, GetWorldPosition(x, y));
                    count++;
                }
            }
        }
        staticVarAccsessorTileMap = new MyTileMap();
    }
    public MyTileMap staticVarAccsessorTileMap = new MyTileMap(); // only used for accessing and setting static var for map tile sprites
    private MyGrid<GameObject> grid;
    private Sprite currentSprite; // this is the sprite which is used for setting a tile sprite
    public static int currentSpriteIndex;
    private SpriteSelection spriteSelection;
    float cellsize;
    Vector3 originposition;

    public void SetTileSprite(Vector3 worldPosition, Sprite sprite)
    {
        GameObject tileMapObject = grid.getValueAtPos(worldPosition);
        if (tileMapObject != null)
        {
            tileMapObject.GetComponent<TileMapObject>().SetTileMapSprite(sprite);
            //save map changes
            for (int x = 0; x < grid.GetWorldWidth(); x++)
            {
                for (int y = 0; y < grid.GetWorldHeight(); y++)
                {
                    if (tileMapObject == grid.gridArray[x, y])
                    {
                        staticVarAccsessorTileMap.setMapSpriteIndex(x,y, currentSpriteIndex);
                        staticVarAccsessorTileMap.useCustomMap();
                    }
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Sprite check = spriteSelection.checkMouseClickOnSpriteSelection(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (check != null)
            {
                currentSprite = check;
            }
            else
            {
                SetTileSprite(Camera.main.ScreenToWorldPoint(Input.mousePosition), currentSprite); // try to set a map tile
            }
        }
    }
    public void setCurrentSpriteIndexValue(int value)
    {
        currentSpriteIndex = value;
    }
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellsize + originposition;
    }
}
