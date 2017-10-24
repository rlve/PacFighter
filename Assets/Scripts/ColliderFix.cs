using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderFix : MonoBehaviour {
    GameObject[] elements;
	// Use this for initialization
	void Start () {
        elements = GameObject.FindGameObjectsWithTag("Walls");

        foreach (var item in elements)
        {
            item.GetComponent<BoxCollider2D>().size = new Vector2(0.48F, 0.48F);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
