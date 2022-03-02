using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterScript : MonoBehaviour
{
    public int health;

    public float speed;
    private Transform target;

    float horizontal;
    float vertical;

    public int damage = 1;

    public bool inRange = false;

    public bool tutorial = false;

    public float timer = 2.0f;

    public float attackTimer;
    public float timeToAttack;

    bool mouseButton;

    public bool seen;

    Vector2 lastPosition;

    Vector2 position;

    Vector2 lookDirection = new Vector2(0, 0);

    Animator animator;

    Rigidbody2D rigidbody2d;

    public GameObject fireballPrefab;

    void Start()
    {
        seen = false;
        inRange = false;
        rigidbody2d = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        position = transform.position;

        attackTimer = timeToAttack;
    }

    void Update()
    {
        if (seen == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            lastPosition = position;

            horizontal = transform.position.x - lastPosition.x;
            vertical = transform.position.y - lastPosition.y;

            Vector2 move = new Vector2(horizontal, vertical);

            if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
            {
                lookDirection.Set(move.x, move.y);
                lookDirection.Normalize();
            }

            animator.SetFloat("Move X", lookDirection.x);
            animator.SetFloat("Move Y", lookDirection.y);

            timer -= Time.deltaTime;
        }

        if (inRange == true)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer < 0)
            {
                attackTimer = timeToAttack;
                Launch();
            }

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Pressed primary button.");
                health = health - 1;

                if (health <= 0)
                {
                    Destroy(gameObject);
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (timer <= 0f)
                {
                    Debug.Log("Pressed secondary button.");
                    timer = 1.0f;
                    health = health - 2;

                    if (health <= 0)
                    {
                        Destroy(gameObject);
                    }
                }
            }

            if (Input.GetMouseButtonDown(2))
            {
                Debug.Log("Pressed middle click.");
            }
        }

        /*if (Input.GetMouseButtonDown(1))
        {
            mouseButton = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            mouseButton = false;
        }*/
    }

    void Launch()
    {
        GameObject fireball = Instantiate(fireballPrefab, rigidbody2d.position, Quaternion.identity);

        Fireball ball = fireball.GetComponent<Fireball>();
        ball.Launch(lookDirection, 200);
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
        if (!tutorial)
        {
            if (player != null)
            {
                player.ChangeHealth(-damage);
            }

            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }
}