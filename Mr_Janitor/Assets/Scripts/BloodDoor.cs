using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDoor : MonoBehaviour
{
    public float tpX = 20f;
    public float tpY = -5f;

    public int bloodTotal;

    Collision2D collision;

    Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void DoorHit()
    {
        if (collision.gameObject.GetComponent<PlayerMovement>().bloodCount >= bloodTotal)
        {
            collision.gameObject.GetComponent<Transform>().position = new Vector2(tpX, tpY);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
            //GetComponent<TPBlackPlay>().Play(animator.SetTrigger("TPScreen"));
            collision = other;
            Invoke("DoorHit", 2);
    }
}