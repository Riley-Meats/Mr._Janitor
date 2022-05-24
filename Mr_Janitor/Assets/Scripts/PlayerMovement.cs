
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    public float speed = 3.0f;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    GameObject menu;
    public GameObject bomb;
    public GameObject bombThrow;
    public GameObject inv1;
    public GameObject inv2;
    public GameObject inv3;
    public Sprite bombSprite;
    bool bombS;

    public bool enemyRange = false;
    public bool inMenu = false;
    public bool noMove = false;

    bool stainRange;

    bool healthUp2Range;
    bool healthUp4Range;
    bool healthUp6Range;

    bool healthUp2BRange;
    bool healthUp4BRange;
    bool healthUp6BRange;

    bool WeakEssence;
    bool SturdyEssence;
    bool StrongEssence;
    bool PowerfulEssence;

    bool inven1;
    bool inven2;
    bool inven3;

    public int maxHealth = 10;
    public float timeInvincible = 2.0f;

    public float health { get { return currentHealth; }}
    public int currentHealth;

    int maxBlood = 100;
    public int currentBlood = 0;

    bool isInvincible;
    float invincibleTimer;

    public float delay = 2.0f;
    public float delayTime;
    float timer;
    public float setTime = 0.75f;

    public int bloodCount;

    Vector2 movePosition = new Vector2(0, 0);

    Vector2 lookDirection = new Vector2(0, 0);

    public Vector2 bombSpawn;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        delayTime = delay;
        timer = setTime;

        menu = transform.GetChild(1).GetChild(1).gameObject;
        menu.SetActive(false);

        

        /*GameObject inventory = Instantiate(bomb, bombSpawn, Quaternion.identity);
        inventory.transform.parent = transform;*/

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 0.75f)
        {
            timer -= Time.deltaTime;
        }

        if (timer < 0f)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            timer = setTime;
        }

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

            if (Input.GetMouseButtonDown(0))
            {
                gameObject.GetComponent<Renderer>().enabled = false;

                timer -= Time.deltaTime;
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

            if (Input.GetKeyDown(KeyCode.E))
            {
                RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position, lookDirection, 1.5f, LayerMask.GetMask("Bomb"));
                if (hit.collider != null)
                {
                    if (!inven1)
                    {
                        inv1.gameObject.GetComponent<Image>().enabled = true;
                        inv1.gameObject.GetComponent<Image>().sprite = bombSprite;
                        bombS = true;
                        inven1 = true;
                    }
                    else if (!inven2 && inven1)
                    {
                        inv2.gameObject.GetComponent<Image>().enabled = true;
                        inv2.gameObject.GetComponent<Image>().sprite = bombSprite;
                        bombS = true;
                        inven2 = true;
                    }
                    else if (!inven3 && inven1 && inven2)
                    {
                        inv3.gameObject.GetComponent<Image>().enabled = true;
                        inv3.gameObject.GetComponent<Image>().sprite = bombSprite;
                        bombS = true;
                        inven3 = true;
                    }
                }
                
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (bombS = true)
                {
                    Launch();
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {

            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {

            }

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

            if (healthUp2BRange == true)
            {
                if (Input.GetKeyDown(KeyCode.E) && bloodCount >= 20)
                {
                    Debug.Log("Pressed E");
                    RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position, lookDirection, 1.5f, LayerMask.GetMask("HealthUp2Buy"));
                    if (hit.collider != null)
                    {
                        Debug.Log("Hit Heatlh");
                        Destroy(hit.collider.gameObject);
                        maxHealth = maxHealth + 2;
                        currentHealth = currentHealth + 2;
                        bloodCount = bloodCount - 20;
                        currentBlood = currentBlood - 20;
                    }
                }
            }

            if (healthUp4BRange == true)
            {
                if (Input.GetKeyDown(KeyCode.E) && bloodCount >= 25)
                {
                    Debug.Log("Pressed E");
                    RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position, lookDirection, 1.5f, LayerMask.GetMask("HealthUp4Buy"));
                    if (hit.collider != null)
                    {
                        Debug.Log("Hit Heatlh");
                        Destroy(hit.collider.gameObject);
                        maxHealth = maxHealth + 4;
                        currentHealth = currentHealth + 4;
                        bloodCount = bloodCount - 25;
                        currentBlood = currentBlood - 25;
                    }
                }
            }

            if (healthUp6BRange == true)
            {
                if (Input.GetKeyDown(KeyCode.E) && bloodCount >= 30)
                {
                    Debug.Log("Pressed E");
                    RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position, lookDirection, 1.5f, LayerMask.GetMask("HealthUp6Buy"));
                    if (hit.collider != null)
                    {
                        Debug.Log("Hit Heatlh");
                        Destroy(hit.collider.gameObject);
                        maxHealth = maxHealth + 6;
                        currentHealth = currentHealth + 6;
                        bloodCount = bloodCount - 30;
                        currentBlood = currentBlood - 30;
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
            //other.GetComponent<FrenchGoat>().seen = true;
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

        if (other.gameObject.tag == "HealthUp2Buy")
        {
            healthUp2BRange = true;
        }

        if (other.gameObject.tag == "HealthUp4Buy")
        {
            healthUp4BRange = true;
        }

        if (other.gameObject.tag == "HealthUp6Buy")
        {
            healthUp6BRange = true;
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
            //other.GetComponent<FrenchGoat>().seen = false;
            enemyRange = false;
        }

        if (other.gameObject.tag == "Stain")
        {
            stainRange = false;
        }

        if (other.gameObject.tag == "HealthUp2Buy")
        {
            healthUp2BRange = false;
        }

        if (other.gameObject.tag == "HealthUp4Buy")
        {
            healthUp4BRange = false;
        }

        if (other.gameObject.tag == "HealthUp6Buy")
        {
            healthUp6BRange = false;
        }
    }

    void Launch()
    {
        GameObject bombs = Instantiate(bombThrow, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Explosion bombed = bombThrow.GetComponent<Explosion>();

        Attack attack = gameObject.GetComponent<Attack>();
        bombed.Launchs(lookDirection, 300);
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
