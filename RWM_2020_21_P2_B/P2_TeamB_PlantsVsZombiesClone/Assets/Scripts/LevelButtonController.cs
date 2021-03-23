using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtonController : MonoBehaviour
{
    [SerializeField]
    GameObject m_imageLock;
    [SerializeField]
    int button_ID;
    private void FixedUpdate()
    {
        if (button_ID == 2)
        {
            if (Data.levelOneComplete)
            {
                m_imageLock.SetActive(false);
            }
        }
        if(button_ID == 3)
        {
            if (Data.levelTwoComplete)
            {
                m_imageLock.SetActive(false);
            }
        }

    }
}
