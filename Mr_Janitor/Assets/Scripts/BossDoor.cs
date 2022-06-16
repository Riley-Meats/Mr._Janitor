using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDoor : MonoBehaviour
{
    public float tpX = 20f;
    public float tpY = -5f;
    float TPTimer = 2.0f;
    float timer;

    bool hit = false;

    public Collider2D boundsTrigger;

    public BigOwlBoss owlBoss;

    public AttackIndicator attackIndicator;

    PlayerMovement player;

    Collision2D collision;

    Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
        GameObject g = GameObject.FindGameObjectWithTag("BigBoyBoss");
        owlBoss = g.GetComponent<BigOwlBoss>();
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
        owlBoss.inRange = true;
        attackIndicator.gameObject.GetComponent<Renderer>().enabled = true;
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

