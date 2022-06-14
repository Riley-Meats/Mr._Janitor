using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIndicator : MonoBehaviour
{
    public GameObject target;

    public Vector2 lookDirection = new Vector2(0, 0);

    public float speed = 1.0f;

    Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.RotateAround(target.transform.position, Vector3.forward, 120 * speed * Time.deltaTime);

        lookDirection.x = transform.position.x - target.transform.position.x;
        lookDirection.y = transform.position.y - target.transform.position.y;

        RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position, lookDirection, 100f, LayerMask.GetMask("BigBirdBoss"));

        if (hit.collider == null)
        {
            transform.RotateAround(target.transform.position, Vector3.forward, 720 * speed * Time.deltaTime);
        }
    }
}