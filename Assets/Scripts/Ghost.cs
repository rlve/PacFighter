using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Character {
    public GameObject destroyEffect;
    int currentDir;
    int frameCounter = 0;
    bool changeDirection = false;

    public override void Start()
    {
        base.Start();
        maxHealth = 1;
        currentHealth = maxHealth;
    }

    public override int GetDirection()
    {
        
        currentDir = Random.Range(0, 4);
        

        if (changeDirection)
        {
            if (currentDir == (int)direction.LEFT)
            {
                currentDir = (int)direction.UP;
            } else
            {
                currentDir++;
            }
            
        }

        return currentDir;
    }

    public void ChangeDirection()
    {
        frameCounter = 0;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Pac>().DecreaseHealth();
        }
        else if (col.gameObject.tag == "Wall")
        {
            ChangeDirection();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Weapon")
        {
            DecreaseHealth();
        }
    }

    public override void DecreaseHealth()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            DestroyEffect();
            Destroy(gameObject);
        }
    }

    public void DestroyEffect()
    {
        var main = destroyEffect.GetComponent<ParticleSystem>().main;

        if (gameObject.name == "gRed")
        {
            main.startColor = new Color(1, 0, 0, 0.7F);
        }
        else if (gameObject.name == "gCyan")
        {
            main.startColor = new Color(0, 1, 1, 0.7F);
        }
        else if (gameObject.name == "gPink")
        {
            main.startColor = new Color(1, 0.4F, 1, 0.7F);
        }
        else if (gameObject.name == "gOrange")
        {
            main.startColor = new Color(1, 0.6F, 0, 0.7F);
        }
        else if (gameObject.name == "gViolet")
        {
            main.startColor = new Color(0.5F, 0, 1, 0.7F);
        }


        Instantiate(destroyEffect, transform.position, transform.rotation);
    }


}
