using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public byte maxHealth;
    public byte currentHealth;

    public float speed;
    public Animator anim;

    public enum direction : int { UP, RIGHT, DOWN, LEFT, STOP }
    public string directionVariable = "direction";

    int currentDir;
    int frameCounter = 0;

    // Use this for initialization
    public virtual void Start () {
        anim = GetComponent<Animator>();
        speed = 2;
    }
	
	// Update is called once per frame
	public virtual void Update () {
        
        Movement(GetDirection());
    }

    public virtual void DecreaseHealth()
    {

    }

    public virtual void IncreaseHealth()
    {

    }

    public void ChangeDirection()
    {
        frameCounter = 0;
    }


    public virtual int GetDirection()
    {
        if (frameCounter == 0 )
        {
            frameCounter = Random.Range(8, 20);
            currentDir = Random.Range(0, 4);
        }
        frameCounter--;

        return currentDir;
    }
    

    void Movement(int dir)
    {
        switch (dir)
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
}
