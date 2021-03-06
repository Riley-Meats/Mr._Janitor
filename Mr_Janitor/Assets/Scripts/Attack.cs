using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    float timer;
    public float setTime = 1.0f;
    public Animator animator;
    public float angle;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        timer = setTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            timer -= Time.deltaTime;
            
            gameObject.GetComponent<Renderer>().enabled = true;

            animator.Play("JanitorAttack");

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - transform.position;

            angle = Vector2.SignedAngle(Vector2.right, direction);
            transform.eulerAngles = new Vector3(0, 0, angle);
        }

        if (timer <= 0f)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            timer = setTime;
        }

        if (timer < 0.75f)
        {
            timer -= Time.deltaTime;
        }

        
    }

    /*void OnTriggerEnter2D(Collision2D other)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Communist")
            {
                
            }
        }
    }*/
}
