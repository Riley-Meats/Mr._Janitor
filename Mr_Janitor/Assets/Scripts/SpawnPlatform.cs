using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{
    float percent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Chance()
    {
        percent = Random.value;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Chance();
        Debug.Log(percent);
    } 
}
