using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pac : Character {
    public byte maxHealth = 5;
    byte currentHealth;

    public UI_handler ui_handler;

    private bool spawned = false;
    private float decay;

    public override void Start()
    {
        base.Start();
    }

    public override int GetDirection()
    {
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

    public override void ManageHealth()
    {
        
    }

    public override void Update()
    {
        base.Update();

        Reset();
        if (Input.GetKey(KeyCode.Z) && !spawned)
        {
            decay = 0.5f;
            spawned = true;
            ui_handler.DecreaseHealth();
        }
        else if (Input.GetKey(KeyCode.X) && !spawned)
        {
            decay = 0.5f;
            spawned = true;
            ui_handler.IncreaseHealth();
        }
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
