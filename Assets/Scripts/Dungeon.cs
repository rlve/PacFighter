using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour {
    public GameObject prefab;
    Vector3 ScreenSize;
    Vector3 BrickSize;
    GameObject brick;




    // Use this for initialization
    void Start ()
    {
        ScreenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        BrickSize = new Vector3(prefab.GetComponent<Renderer>().bounds.size.x, prefab.GetComponent<Renderer>().bounds.size.y, 0);


        //gameObject.transform.position = new Vector3(-ScreenSize.x + (BrickSize.x / 2), ScreenSize.y - (BrickSize.y / 2), 0);
        //MakeBorders(2 * ScreenSize.x);

        //gameObject.transform.position = new Vector3(-ScreenSize.x + (BrickSize.x / 2), -ScreenSize.y + (BrickSize.y / 2), 0);
        //MakeBorders(2 * ScreenSize.x);
    }

 

    void MakeBorders(float size)
    {
        

        for (int i = 0; i < (size / BrickSize.x); i++)
        {
            Instantiate(prefab, new Vector3(transform.position.x + (BrickSize.x * i), transform.position.y), gameObject.transform.rotation, gameObject.transform);
  
        }

       
    }



    

}
