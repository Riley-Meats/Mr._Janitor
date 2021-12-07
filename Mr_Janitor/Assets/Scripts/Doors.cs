using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public float tpX = 20f;
    public float tpY = -5f;

    public float delay = 2.0f;

    float delayTime;

    /*void Start()
    {
        
    }

    void Update()
    {
        
    }*/

    public void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Transform>().position = new Vector2(tpX, tpY);
            }

        Debug.Log("Hit door");
    }
}
