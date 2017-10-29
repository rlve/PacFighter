using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_handler : MonoBehaviour {
    public GameObject[] hearts;
    public GameObject[] gems;
    public GameObject scoreText;
    public GameObject attackText;

    int heart_width = 20;
    int heartsDisplayed;
     
    //dodac wyswietlanie swordow za zdobyte punkty!

    // Use this for initialization
    void Start()
    {
        heartsDisplayed = GameObject.FindGameObjectWithTag("Player").GetComponent<Pac>().maxHealth;
        for (int i = 0; i < heartsDisplayed; i++)
        {
            hearts[i].GetComponent<Image>().enabled = true;
            hearts[i].transform.localPosition += new Vector3((i* heart_width) + (i*5), 0, 0);
        }

        UpdateScore();
    }

    public void DisplayAttackPrompt()
    {
        if (GameObject.Find("Sword") != null)
        {
            attackText.GetComponent<Text>().enabled = true;
        }
        
    }


    public void UpdateScore()
    {
        gems = GameObject.FindGameObjectsWithTag("Gem");

        scoreText.GetComponent<Text>().text = "GEMS LEFT: " + gems.Length.ToString();
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
        UpdateScore();
    }

    public virtual void SetPosition()
    {
    }
}
