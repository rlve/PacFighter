using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public float speed;
    Animator anim;

    public enum direction : int { UP, RIGHT, DOWN, LEFT, STOP }
    string directionVariable = "direction";

    GameObject me;
    int currentDir;
    int frameCounter = 0;
 

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        speed = 2;

        me = this.gameObject;
    }
	
	// Update is called once per frame
	void Update () {

        Movement(GetDirection());     
	}


    public virtual int GetDirection()
    {
        if (this.frameCounter == 0 )
        {
            frameCounter = Random.Range(8, 20);
            currentDir = Random.Range(0, 4);
        }
        frameCounter--;

        Debug.Log(new
        {
            me.name,
            currentDir,
            frameCounter
        });

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
