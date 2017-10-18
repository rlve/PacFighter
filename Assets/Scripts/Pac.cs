using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pac : MonoBehaviour {

    public float speed = 4.0F;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
	}

    void Movement()
    {
        if (Input.GetKey(KeyCode.D))
        { 
            transform.Translate(speed * Time.deltaTime, 0 , 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        else if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
    }


}
