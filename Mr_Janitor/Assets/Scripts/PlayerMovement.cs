using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3.0f;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    public int maxHealth = 10;
    public float timeInvincible = 2.0f;

    public int health { get { return currentHealth; }}
    public int currentHealth;

    bool isInvincible;
    float invincibleTimer;

    public float delay = 2.0f;
    public float delayTime;

    bool TP = false;

    Vector2 movePosition = new Vector2(0, 0);

    Vector2 lookDirection = new Vector2(0, 0);

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
	animator = GetComponent<Animator>();

        delayTime = delay;

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            //lookDirection.Normalize();
        }

        animator.SetFloat("Move X", lookDirection.x);
        animator.SetFloat("Move Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (move.magnitude == 0)
        {
            animator.Play("Idle");
        }

        if (TP == true)
        {
            delayTime -= Time.deltaTime;

            if (delayTime < 0.5)
            {
                position = movePosition;
                Debug.Log(position);
                delayTime = delay;
                TP = false;
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

    public void ChangeHealth (int amount) 
    {
	if (amount < 0) 
	{ 
	     if (isInvincible)
		return;

	     isInvincible = true;
	     invincibleTimer = timeInvincible;
	}

	currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
	Debug.Log(currentHealth + "/" + maxHealth);
    }

    public void DoorHit(Vector2 doorPos)
    {
        TP = true;
        movePosition = doorPos;
        Debug.Log(movePosition);
    }
}
