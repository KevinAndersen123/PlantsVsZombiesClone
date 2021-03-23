using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopButtonScript : MonoBehaviour
{
    public GameObject shop;
    GameObject item;
    int itemCost;
    public Sprite itemSprite;
    ColorBlock cb;

    public Color cantAffordColor;
    public Color canAffordColor;
    Color defaultColor;

    public int fontSize;

    GameObject timerImager;
    public Color timerColor;
    int itemCooldown;
    float timeLeft;

    bool canBuy;
    float timerH;
    RectTransform trt;

    // Start is called before the first frame update
    void Start()
    {

        cb = GetComponent<Button>().colors;
        defaultColor = cb.normalColor;
        canBuy = true;
    }

    void Update()
    {
        if (!canBuy)
        {
            timer();
        }

    }
    public void SetItem(GameObject t_item, Vector2 t_buttonSize)
    {
        item = t_item;
        itemCost = item.GetComponent<ItemObjectScript>().GetPriceOfItem();
        itemSprite = item.GetComponent<ItemObjectScript>().GetItemSpriteFromScript();
        InitButton(t_buttonSize);
        InitTimer();
    }


    void InitButton(Vector2 t_buttonSize)
    {
        GetComponent<Image>().sprite = itemSprite;
        RectTransform panelRectT = GetComponent<RectTransform>();
        panelRectT.sizeDelta = t_buttonSize;
        SetCostString();

    }

    public GameObject GetItem()
    {
        return item;
    }

    public int GetItemPrice()
    {
        return itemCost;
    }

    public void ChangeItemColor()
    {
        if (GetComponentInParent<ShopMenuController>().CanAfford(itemCost))
        {
            cb.normalColor = canAffordColor;
        }
        else if (!GetComponentInParent<ShopMenuController>().CanAfford(itemCost))
        {
            cb.normalColor = cantAffordColor;
        }
        GetComponent<Button>().colors = cb;
    }

    public bool CanPlayerBuyItem()
    {
        if (canBuy)
        {
            if (GetComponentInParent<ShopMenuController>().CanAfford(itemCost))
            {
                ChangeItemColor();
                return true;
            }
        }

        ChangeItemColor();
        return false;
    }

    public void OnItemClicked()
    {
        if (canBuy)
        {
            if (CanPlayerBuyItem())
            {
                GetComponentInParent<ShopMenuController>().BuyItem(item, itemCost);
                timeLeft = itemCooldown;
                canBuy = false;
            }
            else
            {
                print(GetComponentInParent<ShopMenuController>().GetPlayerMoney());
            }
        }
    }

    void SetCostString()
    {
        GetComponentInChildren<Text>().rectTransform.anchoredPosition = new Vector2(0, -57);
        GetComponentInChildren<Text>().fontSize = fontSize;
        GetComponentInChildren<Text>().text = "" + itemCost;
    }


    public void InitTimerImage(RectTransform parentTransform, Vector2 cellsize)
    {
        timerImager = new GameObject();
        timerImager.AddComponent<RectTransform>();
        timerImager.AddComponent<Image>();
        timerImager.name = "timerBox";
        trt = timerImager.GetComponent<RectTransform>();

        timerImager.GetComponent<Image>().color = timerColor;

        trt.anchorMin = new Vector2(0.5f, 1);
        trt.anchorMax = new Vector2(0.5f, 1);
        trt.pivot = new Vector2(0.5f, 1);
        trt.anchoredPosition = new Vector2(0, 50);
        timerImager.transform.SetParent(parentTransform);
        trt.sizeDelta = new Vector2(cellsize.x, 0.0f);
        timerH = cellsize.y;
    }
    void InitTimer()
    {

        itemCooldown = item.GetComponent<ItemObjectScript>().GetCoolDownTime();
        timeLeft = 0;
    }
    void timer()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            trt.sizeDelta = new Vector2(trt.sizeDelta.x, timerH * (timeLeft / itemCooldown));
        }
        else
        {
            canBuy = true;
            timeLeft = itemCooldown;
        }
    }

    public void ResetTimer()
    {
        canBuy = true;
        timeLeft =0;
        trt.sizeDelta = new Vector2(trt.sizeDelta.x, timerH * (timeLeft / itemCooldown));
        timer();
    }
}
