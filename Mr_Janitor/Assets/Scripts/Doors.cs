using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Doors : MonoBehaviour
{
    public float tpX = 20f;
    public float tpY = -5f;

    Collision2D collision;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
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
        GetComponent<TPBlackPlay>().Play(animator.SetTrigger("TPScreen"));
        collision = other;
        Invoke("DoorHit", 2);
    }
}