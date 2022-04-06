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

    public bool enemyRange = false;
    public bool inMenu = false;
    public bool noMove = false;

    bool stainRange;

    bool healthUp2Range;
    bool healthUp4Range;
    bool healthUp6Range;

    bool WeakEssence;
    bool SturdyEssence;
    bool StrongEssence;
    bool PowerfulEssence;

    public int maxHealth = 10;
    public float timeInvincible = 2.0f;

    public float health { get { return currentHealth; }}
    public int currentHealth;

    int maxBlood = 100;
    int currentBlood = 0;

    bool isInvincible;
    float invincibleTimer;

    public float delay = 2.0f;
    public float delayTime;

    public int bloodCount;

    Vector2 movePosition = new Vector2(0, 0);

    Vector2 lookDirection = new Vector2(0, 0);

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        delayTime = delay;

        menu = transform.GetChild(1).GetChild(1).gameObject;
        menu.SetActive(false);

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        if (inMenu == false && noMove == false)
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
                            currentBlood++;
                            bloodCount++;
                            
                        }
                    }
                }
            }

            BloodCounter.instance.SetValue(currentBlood / (float)maxBlood);

            if (healthUp2Range == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position, lookDirection, 1.5f, LayerMask.GetMask("HealthUp2"));
                    if (hit.collider != null)
                    {
                        Destroy(hit.collider.gameObject);
                        maxHealth = maxHealth + 2;
                        currentHealth = currentHealth + 2;
                    }
                }
            }

            if (healthUp4Range == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position, lookDirection, 1.5f, LayerMask.GetMask("HealthUp4"));
                    if (hit.collider != null)
                    {
                        Destroy(hit.collider.gameObject);
                        maxHealth = maxHealth + 4;
                        currentHealth = currentHealth + 4;
                    }
                }
            }

            if (healthUp6Range == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position, lookDirection, 1.5f, LayerMask.GetMask("HealthUp6"));
                    if (hit.collider != null)
                    {
                        Destroy(hit.collider.gameObject);
                        maxHealth = maxHealth + 6;
                        currentHealth = currentHealth + 6;
                    }
                }
            }
        }

        if (WeakEssence == true && currentHealth != maxHealth)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Pressed E");
                RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position, lookDirection, 1.5f, LayerMask.GetMask("WeakEssence"));
                if (hit.collider != null)
                {
                    Debug.Log("Hit Potion");
                    Destroy(hit.collider.gameObject);
                    //currentHealth = currentHealth + currentHealth * 0.25f;
                    int increase = maxHealth / 4;
                    ChangeHealth(increase);
                    
                    Debug.Log(currentHealth);
                }
            }
        }

        if (SturdyEssence == true && currentHealth != maxHealth)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Pressed E");
                RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position, lookDirection, 1.5f, LayerMask.GetMask("SturdyEssence"));
                if (hit.collider != null)
                {
                    Debug.Log("Hit Potion");
                    Destroy(hit.collider.gameObject);
                    int stuIncrease = maxHealth / 2;
                    ChangeHealth(stuIncrease);
                    Debug.Log(currentHealth);
                }
            }
        }

        if (StrongEssence == true && currentHealth != maxHealth)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Pressed E");
                RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position, lookDirection, 1.5f, LayerMask.GetMask("StrongEssence"));
                if (hit.collider != null)
                {
                    Debug.Log("Hit Potion");
                    Destroy(hit.collider.gameObject);
                    int strIncrease = maxHealth * 3 / 4;
                    ChangeHealth(strIncrease);
                    Debug.Log(currentHealth);
                }
            }
        }

        if (PowerfulEssence == true && currentHealth != maxHealth)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Pressed E");
                RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position, lookDirection, 1.5f, LayerMask.GetMask("PowerfulEssence"));
                if (hit.collider != null)
                {
                    Debug.Log("Hit Potion");
                    Destroy(hit.collider.gameObject);
                    int powIncrease = maxHealth;
                    ChangeHealth(powIncrease);
                    Debug.Log(currentHealth);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !menu.activeSelf)
        {
            menu.SetActive(true);
            inMenu = true;
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menu.activeSelf)
        {
            menu.SetActive(false);
            inMenu = false;
            Time.timeScale = 1;
        }

        if (currentHealth == 0 && !menu.activeSelf)
        {
            menu.SetActive(true);
            inMenu = true;
            Time.timeScale = 0;
        }

        if (inMenu == true)
        {
            animator.Play("Idle");
        }
    }

    void FixedUpdate()
    {
        if (inMenu == false && !noMove)
        {
            Vector2 position = rigidbody2d.position;
            position.x = position.x + speed * horizontal * Time.deltaTime;
            position.y = position.y + speed * vertical * Time.deltaTime;

            rigidbody2d.MovePosition(position);
        }
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
        HealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    public void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<FrenchGoat>().seen = true;
            enemyRange = true;
        }


        if (other.gameObject.tag == "Stain")
        {
            Debug.Log("You touched a stain");
            stainRange = true;
        }

        if (other.gameObject.tag == "HealthUp2")
        {
            healthUp2Range = true;
        }

        if (other.gameObject.tag == "HealthUp4")
        {
            healthUp4Range = true;
        }

        if (other.gameObject.tag == "HealthUp6")
        {
            healthUp6Range = true;
        }

        if (other.gameObject.tag == "WeakEssence")
        {
            WeakEssence = true;
            Debug.Log("Touch potion");
        }

        if (other.gameObject.tag == "SturdyEssence")
        {
            SturdyEssence = true;
            Debug.Log("Touch potion");
        }

        if (other.gameObject.tag == "StrongEssence")
        {
            StrongEssence = true;
            Debug.Log("Touch potion");
        }

        if (other.gameObject.tag == "PowerfulEssence")
        {
            PowerfulEssence = true;
            Debug.Log("Touch potion");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<FrenchGoat>().seen = false;
            enemyRange = false;
        }

        if (other.gameObject.tag == "Stain")
        {
            stainRange = false;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
