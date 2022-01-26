/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtySpot : MonoBehaviour
{
    public bool cleanRange = false;

    public int bloodCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Doors>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cleanRange == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                Debug.Log("Cleaned");
                cleanRange = false;
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered area");
        cleanRange = true;
        bloodCount++;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exited area");
        cleanRange = false;
    }
}*/
