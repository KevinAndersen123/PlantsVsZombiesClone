using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelButtonScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject shovelPrefab;
    bool isSelected;
    void Start()
    {

    }



    public void OnButtonClick()
    {
        isSelected = true;
        shovelPrefab.transform.position = GetMousePosition();
        Instantiate(shovelPrefab);
    }
    public void DeselectShovel() { isSelected = false; }
    public Vector2 GetMousePosition() { return Camera.main.ScreenToWorldPoint(Input.mousePosition); }
}

