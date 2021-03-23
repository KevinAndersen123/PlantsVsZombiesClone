using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectScript : MonoBehaviour
{

    public int priceOfItem;
    public Sprite itemSprite; // if you need to change item sprite
    public string ItemName;
    public int coolDownAfterPurchase;
    public Animator animator;

    Sprite currentAnimationSprite;

    public int actionCoolDownTimer;
    bool isActive;
    int currentActionCoolDownCount;

    bool itemPlaced; // check for if the item has been bought and yet to be placed
    bool itemBought;

    GameObject grid;
    public int itemID; // mainly used to just scale the item correctly due to varying sprite sizes
    // Start is called before the first frame update
    [SerializeField]
    BoxCollider2D col;
    int health;
    bool firing;

    Vector2 colliderOffset;
    float colliderRadius;
    GameObject shop;
    bool cherrySoundPlayed = false;
    void Start()
    {
        if (itemSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = itemSprite;
            currentAnimationSprite = itemSprite;
        }
        firing = false;
        isActive = false;
        itemBought = true;

        currentActionCoolDownCount = actionCoolDownTimer;
        shop = GameObject.FindGameObjectWithTag("Shop");
        grid = GameObject.FindGameObjectWithTag("ItemGrid");
        if (itemID == 1 )
        {
            health = 40;
            transform.localScale = new Vector3(0.3f, 0.3f, 1); // sunflower scaling
            actionCoolDownTimer *= 4;
            currentActionCoolDownCount = actionCoolDownTimer;
            colliderOffset = new Vector2(0, 1.1f);
            colliderRadius = 1.2f;
        }
        if (itemID == 2) // peashooter
        {
            health = 40;
            transform.localScale = new Vector3(0.15f, 0.15f, 1); // peashooter scaling
            colliderOffset = new Vector2(-0.2f, 1.84f);
            colliderRadius = 1.77f;

        }
        if (itemID == 3) // cherrybomb
        {
            health = 100; // destroys itself
            transform.localScale = new Vector3(0.3f, 0.3f, 1); // bomb scaling

        }
        if (itemID == 4) // nut
        {
            health = 250;
            transform.localScale = new Vector3(0.5f, 0.5f, 1); // nut scaling
        }
        if (itemID == 5)
        {
            health = 40;
            transform.localScale = new Vector3(0.2f, 0.2f, 1); // iceflower scaling
            colliderOffset = new Vector2(0, 1.1f);
            colliderRadius = 1.2f;
        }
    }

    private void Update()
    {
        if (isActive == false)
        {
            moveItem();
        }

        switch (itemID)
        {
            case 1: // sunflower
                if (isActive)
                {
                    SunflowerTimer();
                }
                break;
            case 2:
                if (firing && isActive)
                {
                    TimeShooting();
                }

                break;
            case 3:
                if (isActive) // CHERRY GET BIG ER
                {
                   
                    if (GetComponent<CircleCollider2D>().radius <= 5f)
                    {
                        if (!cherrySoundPlayed && GetComponent<CircleCollider2D>().radius > 3)
                        {
                            cherrySoundPlayed = true;
                            FindObjectOfType<AudioManager>().Play("CherryExplosion");
                        }
                        GetComponent<CircleCollider2D>().radius += 3 * Time.deltaTime;
                    }
                    else
                    {
                        Destroy(this.gameObject);                         
                    }
                }
                break;
            case 4:
                break;
            case 5:
                if (firing && isActive)
                {

                    TimeShooting();

                }
                break;
            default:
                break;
        }
    }
    public Sprite GetItemSpriteFromScript()
    {
        if (itemSprite != null)
        {
            return itemSprite;
        }
        else
        {
            return GetComponent<SpriteRenderer>().sprite;
        }
    }

    public string GetItemName() { return ItemName; }

    public int GetCoolDownTime() { return coolDownAfterPurchase; }

    public Sprite GetCurrentAnimationSprite() { return GetComponent<SpriteRenderer>().sprite; }

    public void SetActiveState(bool isA) { isActive = isA; }

    public void setBought(bool t_isBought) { itemBought = t_isBought; }

    public void setPosition(Vector2 t_pos) { transform.position = t_pos; }

    public int GetPriceOfItem() { return priceOfItem; }
    // used to set the tile as parent
    public void setParent(Transform parentTransform) { transform.SetParent(parentTransform); }

    void moveItem()
    {
        GetComponent<DragItem>().MoveItem();

        if (Input.GetMouseButtonDown(0))
        {
            GameObject tile;

            tile = grid.GetComponent<ItemGrid>().CheckForMouseCollision(transform.position);
            if (tile != null) // and check if mouse position is over an empty tile when implemented
            {
                if (tile.transform.childCount == 0)
                {
                    isActive = true;
                    animator.SetBool("isActive", true);

                    // set parent to tile
                    transform.SetParent(tile.transform);
                    transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y - 0.5f, -1.1f);
                  
                  
                    if (itemID == 1)
                    {
                        Data.s_sunflowerCounter++;
                        transform.localScale = new Vector3(0.3f, 0.3f, 1); // sunflower scaling
                    }
                    if (itemID == 2) // peashooter
                    {
                        Data.s_peaShooterCounter++;
                        transform.localScale = new Vector3(0.15f, 0.15f, 1); // pea scaling


                        float column = 10 - tile.transform.position.x / 1.3f;

                        col.size = new Vector2((column) * 6.5f, 0.32f);
                        col.offset = new Vector2(((column) * 6.5f) / 2, 1);
                        transform.SetParent(tile.transform);
                        transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y - 0.5f, -1.1f);

                    }
                    if (itemID == 3) // Cherry
                    {
                        Data.s_cherryCounter++;
                       
                        transform.localScale = new Vector3(0.3f, 0.3f, 1); // scaling
                    }

                    if (itemID == 4) //Wallnut
                    {
                        Data.s_wallnutCounter++;
                    }
                    if (itemID == 5)
                    {
                        Data.s_iceShooterCounter++;
                        transform.localScale = new Vector3(0.15f, 0.15f, 1); // pea scaling


                        float column = 10 - tile.transform.position.x / 1.3f;

                        col.size = new Vector2((column) * 6.5f, 0.32f);
                        col.offset = new Vector2(((column) * 6.5f) / 2, 1);

                    }
                    if (itemID != 3 || itemID != 4)
                    {
                        transform.gameObject.AddComponent<CircleCollider2D>();
                        CircleCollider2D ccol = transform.gameObject.GetComponent<CircleCollider2D>();
                        ccol.radius = colliderRadius;
                        ccol.offset = colliderOffset;
                    }
                    
                }
            }
            else
            {
                ResetButtonTimerOnDelete();
                Destroy(this.gameObject);
                FindObjectOfType<ShopMenuController>().UpdatePlayerMoney(priceOfItem);
            }
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(transform.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive && itemID == 3)
        {
            if (collision.gameObject.tag == ("Zombie"))
            {
               
                collision.gameObject.GetComponent<Zombie>().killZombie();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isActive && itemID == 2 || itemID == 5 && isActive)
        {
            if (collision.gameObject.tag == "Zombie")
            {
                if (!collision.gameObject.GetComponent<Animator>().GetBool("isDead"))
                {
                    firing = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isActive && itemID == 2 || itemID == 5 && isActive)
        {
            if (collision.gameObject.tag == "Zombie")
            {
                firing = false;
            }
        }
    }

    private void TimeShooting()
    {
        if (currentActionCoolDownCount == actionCoolDownTimer)
        {
            animator.SetBool("IsShooting", false);
            firing = false;
        }

        if (currentActionCoolDownCount <= 0)
        {
            GetComponent<PlantAction>().FireBullet();
            animator.SetBool("IsShooting", isActive);
            currentActionCoolDownCount = actionCoolDownTimer;
        }
        currentActionCoolDownCount--;
    }

    private void SunflowerTimer()
    {
        if (currentActionCoolDownCount < 0)
        {
            currentActionCoolDownCount = actionCoolDownTimer;
            GetComponent<SunFlowerAction>().SpawnShine();

        }
        currentActionCoolDownCount--;
    }

    private void ResetButtonTimerOnDelete()
    {
        int buttonNum = 0;
        switch (itemID)
        {
            case 1:
                // sunflower
                buttonNum = 1;
                break;
            case 2:
                buttonNum = 0;
                // pea
                break;
            case 3:
                buttonNum = 2;
                // cherry bomb
                break;
            case 4:
                buttonNum = 3;
                //wallnut
                break;
            case 5:
                buttonNum = 4;
                //new item
                break;
            default:
                break;
        }
        shop.GetComponent<ShopMenuController>().ResetButtonTimer(buttonNum);
    }
    
}
       
