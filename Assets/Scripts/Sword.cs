using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {
    float timer = .15F;
    public GameObject swordEffect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Pac>().canMove = true;
           // Instantiate(swordEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
		
	}
}
