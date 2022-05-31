using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    float horizontal;
    float vertical;

    float timer = 1.0f;
    float deleteTimer;

    bool throws;

    int hits;

    void Start()
    {
        deleteTimer = timer;
    }

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(force * direction);
        throws = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Enemy")
        {
            
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if (throws)
        {
            deleteTimer -= Time.deltaTime;
            if (deleteTimer < 0f)
            {
                //Destroy(gameObject);
            }
        }
    }
}