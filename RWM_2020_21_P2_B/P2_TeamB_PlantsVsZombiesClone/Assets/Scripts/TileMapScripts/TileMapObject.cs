using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Author: Josh Browne

public class TileMapObject :  MonoBehaviour
{
    public GameObject tileGameObject;
    public Canvas canvas;
    private Vector2 mapOrigin;
    private float m_x;
    private float m_y;
    private float cellSize;
    private CanvasRenderer canvasRenderer;
    Image tileImage;
    public TileMapObject(int t_x, int t_y, float cellSize)
    {
        tileGameObject = new GameObject("TileGameObject");
        this.cellSize = cellSize;
        canvasRenderer = tileGameObject.AddComponent<CanvasRenderer>();
        tileImage = tileGameObject.AddComponent<Image>(); //Add the Image Component script
        canvas = tileGameObject.AddComponent<Canvas>();
        tileGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(cellSize, cellSize);
        tileGameObject.GetComponent<RectTransform>().localScale.Set(1, 1, 1);
        canvasRenderer.SetColor(new Color(1f, 1f, 1f, 0f));// is about 100 % transparent(Cant be seen at all, but still active)
    }
    public void init(float cellSize, Vector2 mapOrigin)
    {
        this.mapOrigin = mapOrigin;
        this.cellSize = cellSize;
        canvasRenderer = this.gameObject.AddComponent<CanvasRenderer>();
        tileImage = this.gameObject.AddComponent<Image>(); //Add the Image Component script
        canvas = this.gameObject.AddComponent<Canvas>();
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(cellSize, cellSize);
        //this.gameObject.GetComponent<RectTransform>().lossyScale.Set(1, 1, 1);
        canvasRenderer.SetColor(new Color(1f, 1f, 1f, 0f));// is about 100 % transparent(Cant be seen at all, but still active)
    }

    public void SetTileMapSprite(Sprite t_sprite)
    {
        tileImage.sprite = t_sprite; //Set the Sprite of the Image Component on the new GameObject
        canvasRenderer.SetColor(new Color(1f, 1f, 1f, 1f)); //is a normal sprite
    }

    public void SetXY(float x, float y)
    {
        m_x = x;
        m_y = y;
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MapEditor"))
            this.gameObject.transform.localPosition = new Vector3(m_x + (cellSize / 2), m_y + (cellSize / 2));
        else
            this.gameObject.transform.localPosition = new Vector3(mapOrigin.x + (m_x * cellSize), mapOrigin.y + (m_y * cellSize));
    }
}
