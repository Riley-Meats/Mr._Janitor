using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    int damage = 2;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(force * direction);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            player.ChangeHealth(-damage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
