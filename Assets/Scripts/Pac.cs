using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pac : Character {
    
    public int distantPower = 250;
    public bool canMove;

    public UI_handler ui_handler;
    public GameObject sword;


    bool spawned = false;
    float decay;

    public override void Start()
    {
        base.Start();
        maxHealth = 5;
        currentHealth = maxHealth;
        canMove = true;
        speed = 2;
    }

    public override void Update()
    {
        Movement();

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

    void Attack()
    {
        canMove = false;

        GameObject newSword = Instantiate(sword, transform.position, sword.transform.rotation);
        var swordDir = anim.GetInteger(directionVariable);

        if (swordDir == (int)direction.UP)
        {
            newSword.transform.Rotate(0, 0, 0);
            newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.up * distantPower);
        }
        else if (swordDir == (int)direction.RIGHT)
        {
            newSword.transform.Rotate(0, 0, 270);
            newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.right * distantPower);
        }
        else if (swordDir == (int)direction.DOWN)
        {
            newSword.transform.Rotate(0, 0, 180);
            newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.down * distantPower);
        }
        else if (swordDir == (int)direction.LEFT)
        {
            newSword.transform.Rotate(0, 0, 90);
            //newSword.GetComponent<SpriteRenderer>().flipX = true;
            newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.left * distantPower);
        }
    }


    public override void DecreaseHealth()
    {
        ui_handler.DecreaseHealth();
        if (currentHealth > 0)
        {
            currentHealth--;
        }
        else
        {
            //game over
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
