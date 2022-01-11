using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtySpot : MonoBehaviour
{
    bool inRange = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inRange == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Key pressed");
                inRange = false;
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        inRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        inRange = false;
    }
}
