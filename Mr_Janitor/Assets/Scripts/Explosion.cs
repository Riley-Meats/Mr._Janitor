using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    int damage = 2;

    float timer = 2.0f;
    float deleteTimer;

    int hits;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Launchs(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(force * direction);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Enemy")
        {
            
        }
    }

    void Start()
    {
        deleteTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        deleteTimer -= Time.deltaTime;
        if (deleteTimer < 0f)
        {
            Destroy(gameObject);
        }
    }
}