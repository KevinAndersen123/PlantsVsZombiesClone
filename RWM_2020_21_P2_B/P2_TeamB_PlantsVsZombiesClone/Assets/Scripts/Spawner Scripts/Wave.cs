using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Unity UI changeable
[System.Serializable]
public class Wave
{
    //Naming Wave
    public string waveName;

    //Game Object to be Spawned
    public GameObject gameObject;

    //Amount of Objects in the Wave
    public int amountOfObjects = 1;

    //Heavy Wave Check
    public bool heavyWave;

}
