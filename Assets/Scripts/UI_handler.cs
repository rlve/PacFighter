using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_handler : MonoBehaviour {
    public GameObject[] hearts;
    int heart_width = 20;
    int heartsDisplayed = 5;
     
    //dodac wyswietlanie swordow za zdobyte punkty!

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < heartsDisplayed; i++)
        {
            hearts[i].transform.localPosition += new Vector3((i* heart_width) + (i*5), 0, 0);
        }
    }

    public void DecreaseHealth()
    {
        heartsDisplayed--;
        Hide(hearts[heartsDisplayed]);
    }

    public void IncreaseHealth()
    {
        Show(hearts[heartsDisplayed]);
        heartsDisplayed++;
    }

    void Hide(GameObject obj)
    {
        obj.transform.localScale = new Vector3(0, 0, 0);
    }

    void Show(GameObject obj)
    {
        obj.transform.localScale = new Vector3(0.2F, 0.2F, 0.2F);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public virtual void SetPosition()
    {
    }
}
