using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtySpot : MonoBehaviour
{
    public bool cleanRange = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Doors>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cleanRange == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Key pressed");
                cleanRange = false;
                Destroy(gameObject);
                //GetComponent<Doors>().bloodCount--;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        cleanRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        cleanRange = false;
    }
}
