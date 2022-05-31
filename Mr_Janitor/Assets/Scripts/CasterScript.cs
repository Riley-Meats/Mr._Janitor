using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterScript : MonoBehaviour
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

    public GameObject fireballPrefab;

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
            attackTimer -= Time.deltaTime;
            RotateTowardsPlayer();

            if (attackTimer < 0)
            {
                Launch();
                attackTimer = timeToAttack;
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
        }
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

    void Launch()
    {
        GameObject fireball = Instantiate(fireballPrefab, rigidbody2d.position, Quaternion.identity);

        Fireball ball = fireball.GetComponent<Fireball>();
        ball.Launch(GetDirectionOfPlayer(), 2000);

        attackTimer = timeToAttack;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }
}