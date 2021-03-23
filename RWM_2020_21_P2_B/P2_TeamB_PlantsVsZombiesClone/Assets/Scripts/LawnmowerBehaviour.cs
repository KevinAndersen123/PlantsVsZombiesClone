using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawnmowerBehaviour : MonoBehaviour
{
    GameObject m_gameObject;
    SpriteRenderer spriteRenderer;
    CircleCollider2D collider2D;
    Sprite sprite;
    bool isActive = false;

    public void SetUp(Vector3 position)
    {
        m_gameObject = this.gameObject;
        m_gameObject.name = "Lawnmower";
        //m_gameObject.transform.SetParent(this.gameObject.transform);
        m_gameObject.transform.position = position;
        spriteRenderer = m_gameObject.AddComponent<SpriteRenderer>();
        sprite = Resources.Load<Sprite>("Lawn_mower");
        spriteRenderer.sprite = sprite;
        spriteRenderer.sortingOrder = 1;

        //m_gameObject.AddComponent<Rigidbody2D>();
        collider2D = m_gameObject.AddComponent<CircleCollider2D>();
        collider2D.isTrigger = true;
        collider2D.radius = 0.5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "floatyboi(Clone)")
        {
            collision.gameObject.GetComponent<FloatingZombie>().health  = -100;
            return;
        }

        if (collision.gameObject.tag == "Zombie")
        {
            isActive = true;
            FindObjectOfType<AudioManager>().Play("LawnMower");
            collision.gameObject.GetComponent<Zombie>().killZombie();
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (isActive && Time.timeScale > 0)
        {
            this.m_gameObject.transform.position = this.m_gameObject.transform.position + new Vector3(0.05f, 0, 0);

            if (this.m_gameObject.transform.position.x > 25)
            {
                Destroy(this.gameObject); // when off screen, destroy object
            }
        }
    }
}
