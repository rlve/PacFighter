using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pac : Character {
    public GameManager gameMenager;
    public GameObject sword;
    SpriteRenderer sr;

    GameObject[] enemies;
    List<BoxCollider2D> collidersToIgnore = new List<BoxCollider2D>();

    public bool canMove;
    public bool canAttack;
    public int swordPower;
    public bool invincible;
    float invTimer;

    bool spawned = false;
    float decay;

    public override void Start()
    {
        base.Start();
        maxHealth = 3;
        currentHealth = maxHealth;
        canMove = false;
        speed = 2;
        swordPower = 250;
        invincible = false;
        invTimer = 1.5F;
        canAttack = false;
        anim.SetInteger(directionVariable, (int)direction.RIGHT);
        gameMenager = GameObject.Find("Canvas").GetComponent<GameManager>();

        sr = GetComponent<SpriteRenderer>();

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            collidersToIgnore.Add(enemy.GetComponent<BoxCollider2D>());
        }
    }

    public override void Update()
    {
        if (canMove)
        {
            Movement();
        }
        InvincibleHandler();

        // Delay between attacks
        ResetDecay();
        if (Input.GetKeyDown(KeyCode.Space) && !spawned)
        {
            decay = 0.5f;
            spawned = true;
            Attack();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //invincible time after collision with ghost
        if (col.gameObject.tag == "Enemy")
        {
            if (!invincible)
            {
                DecreaseHealth();
                invincible = true;

                foreach (var enemyCollider in collidersToIgnore)
                {
                    Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), enemyCollider);
                }
            }

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // increase ghosts speed on collected gem 
        if (col.gameObject.tag == "Gem")
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                enemy.GetComponent<Ghost>().IncreaseSpeed();
            }
            Destroy(col.gameObject);
        }
        // collect sword to kill ghosts! 
        else if (col.gameObject.name == "Sword") 
        {
            canAttack = true;
            gameMenager.DisplayAttackPrompt();
            Destroy(col.gameObject);
        }
    }

    void Attack()
    {
        if (canAttack)
        {
            canMove = false;

            GameObject newSword = Instantiate(sword, transform.position, sword.transform.rotation);
            var swordDir = anim.GetInteger(directionVariable);

            if (swordDir == (int)direction.UP)
            {
                newSword.transform.Rotate(0, 0, 0);
                newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.up * swordPower);
            }
            else if (swordDir == (int)direction.RIGHT)
            {
                newSword.transform.Rotate(0, 0, 270);
                newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.right * swordPower);
            }
            else if (swordDir == (int)direction.DOWN)
            {
                newSword.transform.Rotate(0, 0, 180);
                newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.down * swordPower);
            }
            else if (swordDir == (int)direction.LEFT)
            {
                newSword.transform.Rotate(0, 0, 90);
                newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.left * swordPower);
            }
        }

    }

    void InvincibleHandler()
    {
        if (invincible == true)
        {
            invTimer -= Time.deltaTime;

            if (!gameMenager.gameOver)
            {
                Flicker();
            }

            if (invTimer <= 0)
            {
                invincible = false;
                invTimer = 1F;
                sr.enabled = true;

                foreach (var enemyCollider in collidersToIgnore)
                {
                    Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), enemyCollider, false);
                }
            }
        }
    }

    void Flicker()
    {
        if (sr.enabled == true) sr.enabled = false;
        else if (sr.enabled == false) sr.enabled = true;
    }

    public override void DecreaseHealth()
    {
        
        if (currentHealth > 0)
        {
            currentHealth--;
            gameMenager.DecreaseHearts();
        }
        else
        {
            canMove = false;
            gameMenager.gameOver = true;   
        }
    }

    public override void Movement()
    {
        switch (GetDirection())
        {
            case (int)direction.UP:
                transform.Translate(0, speed * Time.deltaTime, 0);
                anim.SetInteger(directionVariable, (int)direction.UP);
                anim.speed = 1;
                break;
            case (int)direction.RIGHT:
                transform.Translate(speed * Time.deltaTime, 0, 0);
                anim.SetInteger(directionVariable, (int)direction.RIGHT);
                anim.speed = 1;
                break;
            case (int)direction.DOWN:
                transform.Translate(0, -speed * Time.deltaTime, 0);
                anim.SetInteger(directionVariable, (int)direction.DOWN);
                anim.speed = 1;
                break;
            case (int)direction.LEFT:
                transform.Translate(-speed * Time.deltaTime, 0, 0);
                anim.SetInteger(directionVariable, (int)direction.LEFT);
                anim.speed = 1;
                break;
            default:
                anim.speed = 0;
                break;
        }
    }

    public override int GetDirection()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            return (int)direction.UP;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            return (int)direction.RIGHT;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            return (int)direction.DOWN;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            return (int)direction.LEFT;
        }
        else return (int)direction.STOP;
    }

    //handling delay between getting keyboard input
    private void ResetDecay()
    {
        if (spawned && decay > 0)
        {
            decay -= Time.deltaTime;
        }
        if (decay < 0)
        {
            decay = 0;
            spawned = false;
        }
    }


}
