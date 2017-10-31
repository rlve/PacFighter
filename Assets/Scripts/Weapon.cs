using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    float timer = .15F;
	
	void Update () {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Pac>().canMove = true;
            Destroy(gameObject);
        }
		
	}


}
