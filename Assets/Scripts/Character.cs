using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    public Animator anim;

    public byte maxHealth;
    public byte currentHealth;
    public float speed;

    public enum direction : int { UP, RIGHT, DOWN, LEFT, STOP }
    public string directionVariable = "direction";

    public virtual void Start () {
        anim = GetComponent<Animator>();        
    }
	
	public virtual void Update () {
        
    }

    public virtual void DecreaseHealth()
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
