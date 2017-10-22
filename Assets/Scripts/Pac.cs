using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pac : Character {

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



}
