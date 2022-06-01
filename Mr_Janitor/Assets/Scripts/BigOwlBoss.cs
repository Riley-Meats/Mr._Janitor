using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigOwlBoss : MonoBehaviour
{
    public GameObject Player;

    public CircleCollider2D range;

    public int health;

    public float speed;
    private Transform target;

    float horizontal;
    float vertical;

    public int damage = 1;

    public bool inRange = false;

    public float timer = 2.0f;

    public float attackTimer;
    public float timeToAttack = 2.5f;

    bool mouseButton;

    public bool seen;

    Vector2 lastPosition;

    Vector2 position;

    Vector2 lookDirection = new Vector2(0, 0);

    Rigidbody2D rigidbody2d;

    void Start()
    {
        seen = false;
        inRange = false;
        rigidbody2d = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        position = transform.position;

        attackTimer = timeToAttack;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) < .02f)
        {
            inRange = true;
        }
        if (inRange == true)
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

    void Follow()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }
}
