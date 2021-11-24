using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    private Transform target;

    float horizontal;
    float vertical;

    Vector2 lookDirection = new Vector2(0, 0);

    Animator animator;

    Rigidbody2D rigidbody2d;

    void Start () 
 {
     rigidbody2d = GetComponent<Rigidbody2D>();
     target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
     animator = GetComponent<Animator>();
    }
 
 void Update () 
 {
     transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Move X", lookDirection.x);
        animator.SetFloat("Move Y", lookDirection.y);
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other) 
 {
     PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();

     if (player != null)
     {
	 player.ChangeHealth(-1);
     }

     Destroy(gameObject);
 }

    void OnMouseDown()
    {
        Destroy(gameObject);
    }
}