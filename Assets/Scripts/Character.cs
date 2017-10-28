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

    // Use this for initialization
    public virtual void Start () {
        anim = GetComponent<Animator>();
        speed = 2;
    }
	
	// Update is called once per frame
	public virtual void Update () {
        
    }

    public virtual void DecreaseHealth()
    {

    }

    public virtual void IncreaseHealth()
    {

    }


    public virtual int GetDirection()
    {
        return 0;
    }


    public virtual void Movement()
    {



    }
}
