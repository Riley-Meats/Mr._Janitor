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

    bool hit = false;

    public Collider2D boundsTrigger;

    PlayerMovement player;

    Collision2D collision;

    Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (hit == true)
        {
            timer -= Time.deltaTime;
        }

        if (timer > 0 && hit)
        {
            player.noMove = true;
        }
        else if (timer <= 0 && hit)
        {
            player.noMove = false;
            hit = false;
        }
    }

    public void DoorHit()
    {
        player.gameObject.transform.position = new Vector2(tpX, tpY);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        timer = TPTimer;
        hit = true;
        player = other.gameObject.GetComponent<PlayerMovement>();
        //GetComponent<TPBlackPlay>().Play(animator.SetTrigger("TPScreen"));
        Invoke("DoorHit", 2);
    }
}

