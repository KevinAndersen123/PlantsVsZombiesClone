using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayScore : MonoBehaviour
{
    public Text theScore;

    void Start()
    {

        theScore.text = "Change the text";
    }


}