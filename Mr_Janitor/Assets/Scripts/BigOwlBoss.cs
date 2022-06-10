using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigOwlBoss : MonoBehaviour
{
    public GameObject Player;
    public GameObject T1;

    public CircleCollider2D range;

    public int health;
    int damage = 4;

    public float speed;
    private Transform target;

    float horizontal;
    float vertical;

    public bool inRange = false;
    bool hit;

    public float timer = 2.5f;
    public float timerTimer = 6.0f;
    bool time1 = true;
    bool time2;

    public float attackTimer;
    public float timeToAttack = 2.5f;

    bool mouseButton;

    public bool seen;

    public PlayerMovement playerMovement;

    Vector2 lastPosition;

    Vector2 position;

    public Vector2 lookDirection = new Vector2(0, 0);

    Rigidbody2D rigidbody2d;

    void Start()
    {
        seen = false;
        inRange = false;
        rigidbody2d = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        position = transform.position;
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        playerMovement = g.GetComponent<PlayerMovement>();
        GameObject t1 = GameObject.FindGameObjectWithTag("Target1");

        attackTimer = timeToAttack;
    }

    void Update()
    {
        if (inRange)
        {
            if (time1)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    time1 = false;
                    time2 = true;
                    timer = 2.5f;
                    GetDirectionOfPlayer();
                }
            }
            
            if (time2)
            {
                timerTimer -= Time.deltaTime;
                if (timerTimer <= 0)
                {
                    time1 = true;
                    time2 = false;
                    timerTimer = 6.0f;
                }
                if (timerTimer > 0f)
                {
                    if (Vector3.Distance(transform.position, Player.transform.position) < .02f)
                    {
                        inRange = true;
                    }
                    if (inRange == true)
                    {
                        if (!hit)
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

                            attackTimer -= Time.deltaTime;
                        }
                        RotateTowardsPlayer();
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
                        Debug.Log("Pressed secondary button.");
                        health = health - 2;

                        if (health <= 0)
                        {
                            Destroy(gameObject);
                        }
                    }
                }
            }
            if (hit)
            {
                RotateTowardsTarget();
                HitAndRun();
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    void RotateTowardsPlayer()
    {
        float angle = Mathf.Atan2(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, -angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200.0f * Time.deltaTime);
    }

    Vector2 GetDirectionOfPlayer()
    {
        Vector2 direction = new Vector2(0, 0);

        direction.x = (Player.transform.position.x - gameObject.transform.position.x) / range.radius;
        direction.y = (Player.transform.position.y - gameObject.transform.position.y) / range.radius;

        return direction;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        playerMovement.ChangeHealth(-damage);
        hit = true;
    }

    Vector2 HitAndRun()
    {
        Vector2 targetDirection = new Vector2(0, 0);

        targetDirection.x = (T1.transform.position.x - gameObject.transform.position.x) / range.radius;
        targetDirection.y = (T1.transform.position.y - gameObject.transform.position.y) / range.radius;

        return targetDirection;
    }

    void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(T1.transform.position.x - transform.position.x, T1.transform.position.y - transform.position.y) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, -angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200.0f * Time.deltaTime);
    }
}