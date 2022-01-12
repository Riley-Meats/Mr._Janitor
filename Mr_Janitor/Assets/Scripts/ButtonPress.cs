using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    public bool buttonRange = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonRange == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Key pressed");
                buttonRange = false;
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        buttonRange = true;
    }

}
