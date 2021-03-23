using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Author: Josh Browne
//Date: Jan 2021

public class SpriteSelection : MonoBehaviour
{

    public GameObject canvasGameObject;
    public GameObject selectionPanelGameObject;  // main obj
    public GameObject scrollArea;
    public GameObject EventSystem;

    public GameObject[] spriteGameObjects;
    public Sprite[] spriteArray;
    public Image parentPanel;
    
    private CanvasRenderer canvasRenderer;
    public GameObject imageContainer;

    public GameObject prefabButtonToMenu;

    private MyTileMapEditor mapEditorStaticVarAccessor = new MyTileMapEditor();
    public SpriteSelection()
    {
    }
    //Constructor
    public SpriteSelection(float x, float y, float panelWidth, float panelHeight, float cellSize)
    {
        // load sprite assets
        spriteArray = Resources.LoadAll<Sprite>("Sprites/");
        spriteGameObjects = new GameObject[spriteArray.Length];

        // Top Obj - Canvas
        canvasGameObject = new GameObject("Canvas Game Object");
        canvasGameObject.AddComponent<RectTransform>();
        canvasGameObject.GetComponent<RectTransform>().rect.size.Set(panelWidth, panelHeight);
        //canvasGameObject.transform.position = new Vector2(x, y);
        canvasGameObject.AddComponent<Canvas>();
        canvasGameObject.AddComponent<CanvasScaler>();
        canvasGameObject.AddComponent<GraphicRaycaster>();
        Instantiate(prefabButtonToMenu, canvasGameObject.transform); //Add in button that brings u back to menu

        // Event System object (needed to detect mouse input)
        EventSystem = new GameObject("Event System Object");
        EventSystem.AddComponent<EventSystem>();
        EventSystem.AddComponent<StandaloneInputModule>();
        EventSystem.AddComponent<BaseInput>();
        EventSystem.transform.parent = canvasGameObject.transform;

        // second level - Panel obj
        selectionPanelGameObject = new GameObject("Selection Panel GameObject");
        selectionPanelGameObject.transform.parent = canvasGameObject.transform;
        selectionPanelGameObject.AddComponent<Image>();
        selectionPanelGameObject.GetComponent<Image>().enabled = false;


        // third lvl - Scroll area
        scrollArea = new GameObject("Scroll Area");
        scrollArea.transform.parent = selectionPanelGameObject.transform;
        scrollArea.AddComponent<ScrollRect>();//.content = imageContainer.GetComponent<RectTransform>();
        scrollArea.AddComponent<Image>();
        scrollArea.GetComponent<Image>().enabled = false;
        scrollArea.GetComponent<RectTransform>().position = new Vector2(x, y);
        //scrollArea.GetComponent<RectTransform>().sizeDelta.Set(panelWidth, panelHeight);
        scrollArea.GetComponent<ScrollRect>().horizontal = false;
        scrollArea.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 24);

        // fourth lvl - Container
        imageContainer = new GameObject("Image Container");
        imageContainer.transform.parent = scrollArea.transform;
        imageContainer.AddComponent<RectTransform>();
        scrollArea.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 63);
        imageContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        imageContainer.AddComponent<VerticalLayoutGroup>();
        imageContainer.GetComponent<VerticalLayoutGroup>().spacing = 1;
        imageContainer.GetComponent<VerticalLayoutGroup>().childControlWidth = false;
        imageContainer.GetComponent<VerticalLayoutGroup>().childControlHeight = false;
        imageContainer.GetComponent<VerticalLayoutGroup>().childForceExpandWidth = false;
        imageContainer.GetComponent<VerticalLayoutGroup>().childForceExpandHeight = false;
        imageContainer.GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
        imageContainer.GetComponent<VerticalLayoutGroup>().padding.left = 2;
        imageContainer.GetComponent<VerticalLayoutGroup>().padding.top = 9;
        imageContainer.AddComponent<Image>();
        imageContainer.GetComponent<Image>().enabled = false;
        // set scroll stuff
        scrollArea.GetComponent<ScrollRect>().content = imageContainer.GetComponent<RectTransform>();

        int count = 0;
        //create the image objects and set parent to panel
        foreach (Sprite sprite in spriteArray)
        {
            GameObject NewObj = new GameObject("Sprite Selection Item"); //Create the GameObject
            NewObj.transform.SetParent(imageContainer.transform);
            NewObj.AddComponent<BoxCollider2D>();
            NewObj.transform.position = new Vector2(x + 1.25f, cellSize * count + .5f * count + 2);

            Image NewImage = NewObj.AddComponent<Image>(); //Add the Image Component script
            NewImage.sprite = sprite; //Set the Sprite of the Image Component on the new GameObject
            //NewObj.GetComponent<RectTransform>().SetParent(selectionPanelGameObject.GetComponent<RectTransform>()); //Assign the newly created Image GameObject as a Child of the Parent Panel.
            NewObj.GetComponent<RectTransform>().sizeDelta = new Vector2(cellSize, cellSize);
            spriteGameObjects[count] = NewObj; // push to array for later use
            count++; // increment
        }

    }

    public Sprite checkMouseClickOnSpriteSelection(Vector3 t_mousePos)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = t_mousePos;
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                Vector3 clickedSpritePos = hit.collider.gameObject.transform.position;
                int count = 0;
                foreach (GameObject gameObj in spriteGameObjects)
                {
                    if (gameObj.transform.position == clickedSpritePos)
                    {
                        mapEditorStaticVarAccessor.setCurrentSpriteIndexValue(count);
                        return gameObj.GetComponent<Image>().sprite;
                    }
                    count++;
                }
            }
        }
        return null;
    }

    public void init(float x, float y, float panelWidth, float panelHeight, float cellSize)
    {
        // load sprite assets
        spriteArray = Resources.LoadAll<Sprite>("Sprites/");
        spriteGameObjects = new GameObject[spriteArray.Length];

        // Top Obj - Canvas
        canvasGameObject = new GameObject("Canvas Game Object");
        canvasGameObject.AddComponent<RectTransform>();
        canvasGameObject.GetComponent<RectTransform>().rect.size.Set(panelWidth, (spriteArray.Length + 1) * cellSize + 50);
        //canvasGameObject.transform.position = new Vector2(x, y);
        canvasGameObject.AddComponent<Canvas>();
        canvasGameObject.AddComponent<CanvasScaler>();
        canvasGameObject.AddComponent<GraphicRaycaster>();
        Instantiate(prefabButtonToMenu, canvasGameObject.transform); //Add in button that brings u back to menu

        // Event System object (needed to detect mouse input)
        EventSystem = new GameObject("Event System Object");
        EventSystem.AddComponent<EventSystem>();
        EventSystem.AddComponent<StandaloneInputModule>();
        EventSystem.AddComponent<BaseInput>();
        EventSystem.transform.parent = canvasGameObject.transform;

        // second level - Panel obj
        selectionPanelGameObject = new GameObject("Selection Panel GameObject");
        selectionPanelGameObject.transform.parent = canvasGameObject.transform;
        selectionPanelGameObject.AddComponent<Image>();
        selectionPanelGameObject.GetComponent<Image>().enabled = false;


        // third lvl - Scroll area
        scrollArea = new GameObject("Scroll Area");
        scrollArea.transform.parent = selectionPanelGameObject.transform;
        scrollArea.AddComponent<ScrollRect>();//.content = imageContainer.GetComponent<RectTransform>();
        scrollArea.AddComponent<Image>();
        scrollArea.GetComponent<Image>().enabled = false;
        scrollArea.GetComponent<RectTransform>().position = new Vector2(x, 0);
        //scrollArea.GetComponent<RectTransform>().sizeDelta.Set(panelWidth, panelHeight);
        scrollArea.GetComponent<ScrollRect>().horizontal = false;
        scrollArea.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 10);

        // fourth lvl - Container
        imageContainer = new GameObject("Image Container");
        imageContainer.transform.parent = scrollArea.transform;
        imageContainer.AddComponent<RectTransform>();
        imageContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(100, (spriteArray.Length) * (cellSize + 1));
        imageContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        imageContainer.AddComponent<VerticalLayoutGroup>();
        imageContainer.GetComponent<VerticalLayoutGroup>().spacing = 1;
        imageContainer.GetComponent<VerticalLayoutGroup>().childControlWidth = false;
        imageContainer.GetComponent<VerticalLayoutGroup>().childControlHeight = false;
        imageContainer.GetComponent<VerticalLayoutGroup>().childForceExpandWidth = false;
        imageContainer.GetComponent<VerticalLayoutGroup>().childForceExpandHeight = false;
        imageContainer.GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
        imageContainer.GetComponent<VerticalLayoutGroup>().padding.left = 2;
        imageContainer.GetComponent<VerticalLayoutGroup>().padding.top = 0;
        imageContainer.AddComponent<Image>();
        imageContainer.GetComponent<Image>().enabled = false;
        // set scroll stuff
        scrollArea.GetComponent<ScrollRect>().content = imageContainer.GetComponent<RectTransform>();

        int count = 0;
        //create the image objects and set parent to panel
        foreach (Sprite sprite in spriteArray)
        {
            //sprite.bounds.size.Set(1, 1, 1);
            GameObject NewObj = new GameObject("Sprite Selection Item"); //Create the GameObject
            NewObj.transform.SetParent(imageContainer.transform);
            NewObj.AddComponent<BoxCollider2D>();
            NewObj.transform.position = new Vector2(x + 1.25f, cellSize * count + .5f * count);

            Image NewImage = NewObj.AddComponent<Image>(); //Add the Image Component script
            NewImage.sprite = sprite; //Set the Sprite of the Image Component on the new GameObject
            //NewObj.GetComponent<RectTransform>().SetParent(selectionPanelGameObject.GetComponent<RectTransform>()); //Assign the newly created Image GameObject as a Child of the Parent Panel.
            NewObj.GetComponent<RectTransform>().sizeDelta = new Vector2(cellSize, cellSize);
            spriteGameObjects[count] = NewObj; // push to array for later use
            count++; // increment
        }
    }
}
