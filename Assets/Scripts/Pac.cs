using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pac : Character {
    
    public int swordPower;
    public bool canMove;

    SpriteRenderer sr;
    public UI_handler ui_handler;
    public GameObject sword;

    public bool invincible;
    float invTimer;

    GameObject[] enemies;
    List<BoxCollider2D> collidersToIgnore = new List<BoxCollider2D>();

    public bool canAttack;

    bool spawned = false;
    float decay;

    public override void Start()
    {
        base.Start();
        maxHealth = 3;
        currentHealth = maxHealth;
        canMove = true;
        speed = 2;
        swordPower = 250;
        invincible = false;
        invTimer = 1.5F;
        canAttack = false;

        sr = GetComponent<SpriteRenderer>();

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            collidersToIgnore.Add(enemy.GetComponent<BoxCollider2D>());
        }
    }

    public override void Update()
    {
        Movement();
        InvincibleHandler();

        if (Input.GetKeyDown(KeyCode.Space) && !spawned)
        {
            decay = 0.5f;
            spawned = true;
            Attack();
        }

        Reset();
        if (Input.GetKey(KeyCode.Z) && !spawned)
        {
            decay = 0.5f;
            spawned = true;
            DecreaseHealth();
        }
        else if (Input.GetKey(KeyCode.X) && !spawned)
        {
            decay = 0.5f;
            spawned = true;
            IncreaseHealth();
        }
    }

    void Flicker()
    {
        if (sr.enabled == true) sr.enabled = false;
        else if (sr.enabled == false) sr.enabled = true;
    }

    void InvincibleHandler()
    {
        if (invincible == true)
        {
            invTimer -= Time.deltaTime;

            if (!ui_handler.gameOver)
            {
                Flicker();
            }
            

            if (invTimer<=0)
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

    void OnCollisionEnter2D(Collision2D col)
    {
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
        if (col.gameObject.tag == "Gem")
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                enemy.GetComponent<Ghost>().IncreaseSpeed();
            }
            Destroy(col.gameObject);
        }
        else if (col.gameObject.name == "Sword") 
        {
            canAttack = true;
            ui_handler.DisplayAttackPrompt();
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
                //newSword.GetComponent<SpriteRenderer>().flipX = true;
                newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.left * swordPower);
            }
        }

    }

    public override void DecreaseHealth()
    {
        
        if (currentHealth > 0)
        {
            currentHealth--;
            ui_handler.DecreaseHealth();
        }
        else
        {
            canMove = false;
            ui_handler.gameOver = true;
            
        }
    }

    public override void IncreaseHealth()
    {
        ui_handler.IncreaseHealth();
        if (currentHealth<maxHealth)
        {
            currentHealth++;
        }
        
    }



    public override void Movement()
    {
        if (ui_handler.gameOver || ui_handler.gameWin)
        {
            return;
        }

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
        if (canMove == false)
        {
            return (int)direction.STOP;
        }

        if (Input.GetKey(KeyCode.W))
        {
            return (int)direction.UP;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            return (int)direction.RIGHT;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            return (int)direction.DOWN;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            return (int)direction.LEFT;
        }
        else return (int)direction.STOP;
    }


    private void Reset()
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
