using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game : MonoBehaviour
{
    public Transform Spawner;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Spawner);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
