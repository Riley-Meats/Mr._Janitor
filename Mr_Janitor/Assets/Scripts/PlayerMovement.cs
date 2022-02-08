using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    public float speed = 3.0f;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    GameObject menu;

    GameObject storage;

    bool stainRange;

    public int maxHealth = 10;
    public float timeInvincible = 2.0f;

    public int health { get { return currentHealth; }}
    public int currentHealth;

    bool isInvincible;
    float invincibleTimer;

    public float delay = 2.0f;
    public float delayTime;

    public int bloodCount;

    Vector2 movePosition = new Vector2(0, 0);

    Vector2 lookDirection = new Vector2(0, 0);

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
	animator = GetComponent<Animator>();

        delayTime = delay;

        menu = transform.GetChild(1).GetChild(1).gameObject;
        menu.SetActive(false);

        storage = transform.GetChild(1).GetChild(0).gameObject;
        storage.SetActive(false);

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

        if (stainRange == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position, lookDirection, 1.5f, LayerMask.GetMask("Stain"));
                if (hit.collider != null)
                {
                    Debug.Log("Hit Stain");
                    Destroy(hit.collider);
                    DirtySpot stain = hit.collider.GetComponent<DirtySpot>();
                    if (stain != null)
                    {
                        stain.Clean();
                        Debug.Log("Pressed E");
                        bloodCount++;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !menu.activeSelf)
        {
            menu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menu.activeSelf)
        {
            menu.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Q) && !storage.activeSelf)
        {
            storage.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Q) && storage.activeSelf)
        {
            storage.SetActive(false);
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

    public void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyMovement>().seen = true;
        }


        if (other.gameObject.tag == "Stain")
        {
            Debug.Log("You touched a stain");
            stainRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyMovement>().seen = false;
        }

        if (other.gameObject.tag == "Stain")
        {
            stainRange = false;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
