using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    int damage = 2;

    float timer = 2.0f;
    float deleteTimer;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(force * direction);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            player.ChangeHealth(-damage);
        }
        if (other.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
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
