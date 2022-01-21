using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Doors : MonoBehaviour
{
    public float tpX = 20f;
    public float tpY = -5f;

    public float dA = -2.493f;
    public float dB = -0.603f;
    public float dC = 1.136f;
    public float dD = 0.497f;

    //public int bloodCount;

    Bounds door;

    public Collider2D boundsTrigger;

    Collision2D collision;

    Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();

        door = new Bounds(new Vector2(dA, dB), new Vector2(dC, dD));
    }

    void Update()
    {
        
    }

    public void DoorHit()
    {
        collision.gameObject.GetComponent<Transform>().position = new Vector2(tpX, tpY);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
            //GetComponent<TPBlackPlay>().Play(animator.SetTrigger("TPScreen"));
            collision = other;
            Invoke("DoorHit", 2);

        /*while (other.collider.gameObject.tag == "Stain" && other.gameObject.tag == "Enemy")
        {
            bloodCount++;
        }*/
    }

    /*public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BoxCollider2D>().gameObject.tag == "Stain" && other.gameObject.tag == "Enemy")
        {
            bloodCount++;
        }
    }*/
}