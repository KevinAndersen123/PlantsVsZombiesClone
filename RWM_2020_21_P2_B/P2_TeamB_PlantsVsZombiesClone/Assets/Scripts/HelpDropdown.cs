using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpDropdown : MonoBehaviour
{
    public Text m_output;
    public GameObject m_peaShooter;
    public GameObject m_iceShooter;
    public GameObject m_sunFlower;
    public GameObject m_shovel;
    public GameObject m_wallnut;
    public GameObject m_cherry;
    public GameObject m_z;
    public void HandleInputData(int val)
    {
        m_peaShooter.SetActive(false);
        m_iceShooter.SetActive(false);
        m_sunFlower.SetActive(false);
        m_shovel.SetActive(false);
        m_wallnut.SetActive(false);
        m_cherry.SetActive(false);
        m_z.SetActive(false);
        switch (val)
        {

            //peashooter
            case 0:
                m_output.text = "The pea shooter shoots 'pea' objects towards the zombie that just walked onto his row";
                m_peaShooter.SetActive(true);
                break;
            //sunflower
            case 1:
                m_output.text = "The sunflower spawns sunshine currency for which you can purchase more plants";
                m_sunFlower.SetActive(true);
                break;
            //cherry
            case 2:
                m_output.text = "The cherry bomb will explode once placed on tile, Killing any zombies instantly within it's kill radius";
                m_cherry.SetActive(true);
                break;
            //wallnut
            case 3:
                m_output.text = "This tough nut will give you time to place more plants and delay the zombies from reaching your house";
                m_wallnut.SetActive(true);
                break;
            //ice shooter
            case 4:
                m_output.text = "This plant will act the same as the pea shooter except it freezes the zombies for 2 seconds";
                m_iceShooter.SetActive(true);
                break;
            //shovel
            case 5:
                m_output.text = "This item will allow you to destroy plants that you have placed ";
                m_shovel.SetActive(true);
                break;
            //shop
            case 6:
                m_output.text = "To purchase a plant from the shop. Simply left click on the item you want to buy and left click again to place it on the map";
                break;
            //survival mode
            case 7:
                m_output.text = "Levels to easy huh?? See how long you can last with the endless invasion of zombies with all the plants available to you!";
                break;
            //Balloon Zombie
            case 8:
                m_z.SetActive(true);
                m_output.text = "The balloon Zombie is designed to destroy your plants. However there is one plant that could save you...The wallnut";
                break;

        }
    }
}
    
