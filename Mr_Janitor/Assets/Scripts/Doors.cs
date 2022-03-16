using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Doors : MonoBehaviour
{
    public float tpX = 20f;
    public float tpY = -5f;
    float TPTimer = 2.0f;
    float timer;

    public Collider2D boundsTrigger;

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
        collision.gameObject.GetComponent<Transform>().position = new Vector2(tpX, tpY);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        timer = TPTimer;
        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
        player.inMenu = true;
        //GetComponent<TPBlackPlay>().Play(animator.SetTrigger("TPScreen"));
        collision = other;
        Invoke("DoorHit", 2);
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            player.inMenu = false;
            timer = TPTimer;
        }
    }
}