using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawnmowerBehaviour : MonoBehaviour
{
    GameObject m_gameObject;
    Sprite sprite;
    bool isActive = false; 

    public LawnmowerBehaviour(Vector3 position)
    {
        m_gameObject = new GameObject("LawnmowerObject");
        m_gameObject.transform.position = position;
    }

    // Start is called before the first frame update
    void Start()
    {
        sprite = Resources.Load<Sprite>("Lawn_mower");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
