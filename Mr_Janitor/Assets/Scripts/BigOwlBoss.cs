using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigOwlBoss : MonoBehaviour
{
    public GameObject Player;
    public GameObject T1;
    public GameObject T2;
    public GameObject T3;
    public GameObject T4;
    public GameObject T5;
    public GameObject T6;
    public GameObject T7;
    public GameObject T8;

    public CircleCollider2D range;

    public int health;
    int damage = 4;

    public float speed;
    private Transform target;
    private Transform target1;
    private Transform target2;
    private Transform target3;
    private Transform target4;
    private Transform target5;
    private Transform target6;
    private Transform target7;
    private Transform target8;

    public float percent = 1.0f;

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
        target1 = GameObject.FindGameObjectWithTag("Target1").GetComponent<Transform>();
        target2 = GameObject.FindGameObjectWithTag("Target2").GetComponent<Transform>();
        target3 = GameObject.FindGameObjectWithTag("Target3").GetComponent<Transform>();
        target4 = GameObject.FindGameObjectWithTag("Target4").GetComponent<Transform>();
        target5 = GameObject.FindGameObjectWithTag("Target5").GetComponent<Transform>();
        target6 = GameObject.FindGameObjectWithTag("Target6").GetComponent<Transform>();
        target7 = GameObject.FindGameObjectWithTag("Target7").GetComponent<Transform>();
        target8 = GameObject.FindGameObjectWithTag("Target8").GetComponent<Transform>();
        position = transform.position;
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        playerMovement = g.GetComponent<PlayerMovement>();
        GameObject t1 = GameObject.FindGameObjectWithTag("Target1");
        GameObject t2 = GameObject.FindGameObjectWithTag("Target2");
        GameObject t3 = GameObject.FindGameObjectWithTag("Target3");
        GameObject t4 = GameObject.FindGameObjectWithTag("Target4");
        GameObject t5 = GameObject.FindGameObjectWithTag("Target5");
        GameObject t6 = GameObject.FindGameObjectWithTag("Target6");
        GameObject t7 = GameObject.FindGameObjectWithTag("Target7");
        GameObject t8 = GameObject.FindGameObjectWithTag("Target8");

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

                            RotateTowardsPlayer();
                        }
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

                if (inRange)
                {
                    if (percent >= 0f && percent <= .125f)
                    {
                        if (hit)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, target1.position, speed * Time.deltaTime);
                            RotateTowardsTarget1();
                        }
                    }

                    if (percent > .125f && percent <= .25f)
                    {
                        if (hit)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, target2.position, speed * Time.deltaTime);
                            RotateTowardsTarget2();
                        }
                    }

                    if (percent > .25f && percent <= .375f)
                    {
                        if (hit)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, target3.position, speed * Time.deltaTime);
                            RotateTowardsTarget3();
                        }
                    }

                    if (percent > .375f && percent <= .50f)
                    {
                        if (hit)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, target4.position, speed * Time.deltaTime);
                            RotateTowardsTarget4();
                        }
                    }

                    if (percent > .50f && percent <= .625f)
                    {
                        if (hit)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, target5.position, speed * Time.deltaTime);
                            RotateTowardsTarget5();
                        }
                    }

                    if (percent > .625f && percent <= .75f)
                    {
                        if (hit)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, target6.position, speed * Time.deltaTime);
                            RotateTowardsTarget6();
                        }
                    }

                    if (percent > .75f && percent <= .875f)
                    {
                        if (hit)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, target7.position, speed * Time.deltaTime);
                            RotateTowardsTarget7();
                        }
                    }

                    if (percent > .875f && percent <= 1f)
                    {
                        if (hit)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, target8.position, speed * Time.deltaTime);
                            RotateTowardsTarget8();
                        }
                    }
                }
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
        if (other.gameObject.tag == "Player")
        {
            playerMovement.ChangeHealth(-damage);
            hit = true;
            timerTimer = 0f;
            timer = 0f;
            percent = Random.value;
        }

        if (other.gameObject.tag == "Target1")
        {
            timer = 2.5f;
            hit = false;
            time1 = true;
            time2 = false;
        }

        if (other.gameObject.tag == "Target2")
        {
            timer = 2.5f;
            hit = false;
            time1 = true;
            time2 = false;
        }

        if (other.gameObject.tag == "Target3")
        {
            timer = 2.5f;
            hit = false;
            time1 = true;
            time2 = false;
        }

        if (other.gameObject.tag == "Target4")
        {
            timer = 2.5f;
            hit = false;
            time1 = true;
            time2 = false;
        }

        if (other.gameObject.tag == "Target5")
        {
            timer = 2.5f;
            hit = false;
            time1 = true;
            time2 = false;
        }

        if (other.gameObject.tag == "Target6")
        {
            timer = 2.5f;
            hit = false;
            time1 = true;
            time2 = false;
        }

        if (other.gameObject.tag == "Target7")
        {
            timer = 2.5f;
            hit = false;
            time1 = true;
            time2 = false;
        }

        if (other.gameObject.tag == "Target8")
        {
            timer = 2.5f;
            hit = false;
            time1 = true;
            time2 = false;
        }
    }

    void RotateTowardsTarget1()
    {
        float angle = Mathf.Atan2(T1.transform.position.x - transform.position.x, T1.transform.position.y - transform.position.y) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, -angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200.0f * Time.deltaTime);
    }

    void RotateTowardsTarget2()
    {
        float angle = Mathf.Atan2(T2.transform.position.x - transform.position.x, T2.transform.position.y - transform.position.y) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, -angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200.0f * Time.deltaTime);
    }

    void RotateTowardsTarget3()
    {
        float angle = Mathf.Atan2(T3.transform.position.x - transform.position.x, T3.transform.position.y - transform.position.y) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, -angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200.0f * Time.deltaTime);
    }

    void RotateTowardsTarget4()
    {
        float angle = Mathf.Atan2(T4.transform.position.x - transform.position.x, T4.transform.position.y - transform.position.y) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, -angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200.0f * Time.deltaTime);
    }

    void RotateTowardsTarget5()
    {
        float angle = Mathf.Atan2(T5.transform.position.x - transform.position.x, T5.transform.position.y - transform.position.y) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, -angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200.0f * Time.deltaTime);
    }

    void RotateTowardsTarget6()
    {
        float angle = Mathf.Atan2(T6.transform.position.x - transform.position.x, T6.transform.position.y - transform.position.y) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, -angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200.0f * Time.deltaTime);
    }

    void RotateTowardsTarget7()
    {
        float angle = Mathf.Atan2(T7.transform.position.x - transform.position.x, T7.transform.position.y - transform.position.y) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, -angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200.0f * Time.deltaTime);
    }

    void RotateTowardsTarget8()
    {
        float angle = Mathf.Atan2(T8.transform.position.x - transform.position.x, T8.transform.position.y - transform.position.y) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, -angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200.0f * Time.deltaTime);
    }
}