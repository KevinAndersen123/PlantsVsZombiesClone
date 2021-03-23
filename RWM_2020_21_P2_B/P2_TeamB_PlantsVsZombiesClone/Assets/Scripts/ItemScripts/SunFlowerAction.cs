using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlowerAction : MonoBehaviour
{
    public GameObject sunShine;
    // Start is called before the first frame update
    public void SpawnShine()
    {
        Instantiate(sunShine, transform);
    }
}
