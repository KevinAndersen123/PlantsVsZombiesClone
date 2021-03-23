using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;
    public RectTransform Pointer;

    // Start is called before the first frame update
    public void Start()
    {
        slider.value = 0;
    }


    // Update is called once per frame
    public void increaseProgress(float percentComplete)
    {
        slider.value += percentComplete;
    }

    public void placeHeavyWaveMarker(float xPos, float yPos)
    {
        RectTransform flag = Instantiate(Pointer, transform);
        //Vector3 MoveVec = new Vector3(xPos, yPos, 0);
        // Instantiate(Pointer, transform, false);
        //Instantiate(Pointer,transform);
        //Pointer.transform.position = new Vector3(0, 0, 0);
        //Pointer.transform.position = new Vector3(xPos, yPos, 0);

        
        //flag.transform.position = new Vector3(1280, 0, 0);
        flag.transform.position += new Vector3(xPos, yPos, 0);
        //Pointer.transform.position = MoveVec;
        //Pointer.transform.position += new Vector3(xPos, yPos, 0);
    }

}
