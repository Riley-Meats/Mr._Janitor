using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Doors : MonoBehaviour
{
    public float tpX = 20f;
    public float tpY = -5f;

    float delay = 2.0f;

    Animator animator;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<PlayerMovement>().DoorHit(new Vector2(tpX, tpY));
    }
}
