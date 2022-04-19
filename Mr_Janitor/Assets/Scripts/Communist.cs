using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Communist : MonoBehaviour
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

    bool mouseButton;

    public bool seen;

    public GameObject FrenchGoatPrefab;

    Vector2 lookDirection = new Vector2(0, 0);

    Rigidbody2D rigidbody2d;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (seen == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            Vector2 move = new Vector2(horizontal, vertical);

            if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
            {
                lookDirection.Set(move.x, move.y);
                lookDirection.Normalize();
            }

            animator.SetFloat("Move X", lookDirection.x);
            animator.SetFloat("Move Y", lookDirection.y);
        }

        if (inRange == true)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                //Instantiate(FrenchGoatPrefab, transform.position, transform.rotation);
                Spawn();
                timer = 2.0f;
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
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            seen = true;
            inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            seen = false;
            inRange = false;
        }
    }

    void Spawn()
    {
        List<Vector2> SpawnPoints = SpawnGoat();

        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            GameObject frenchGoat = Instantiate(FrenchGoatPrefab, SpawnPoints[i], Quaternion.identity);
        }
        //frenchGoat goat = frenchGoat.GetComponent<frenchGoat>();
    }

    List<Vector2> SpawnGoat()
    {
        Vector2 TLSpawn = new Vector2(0, 0);
        Vector2 TRSpawn = new Vector2(0, 0);
        Vector2 BLSpawn = new Vector2(0, 0);
        Vector2 BRSpawn = new Vector2(0, 0);

        TLSpawn.x = (gameObject.transform.position.x - .1f);
        TLSpawn.y = (gameObject.transform.position.y + .1f);
        TRSpawn.x = (gameObject.transform.position.x + .1f);
        TRSpawn.y = (gameObject.transform.position.y + .1f);
        BLSpawn.x = (gameObject.transform.position.x - .1f);
        BLSpawn.y = (gameObject.transform.position.y - .1f);
        BRSpawn.x = (gameObject.transform.position.x + .1f);
        BRSpawn.y = (gameObject.transform.position.y - .1f);

        List<Vector2> SpawnPoints = new List<Vector2>();
        SpawnPoints.Add(TLSpawn);
        SpawnPoints.Add(TRSpawn);
        SpawnPoints.Add(BLSpawn);
        SpawnPoints.Add(BRSpawn);

        return SpawnPoints;
    }
}