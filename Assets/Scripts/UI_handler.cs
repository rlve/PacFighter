using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_handler : MonoBehaviour {
    public GameObject heartPrefab;
    public GameObject[] hearts;
    public GameObject[] gems;
    public GameObject scoreText;
    public GameObject attackText;
    public GameObject gameOverText;
    public GameObject gameWinText;
    public GameObject startPrompt;
    public GameObject backPrompt;

    int heart_width = 20;
    int heartsToDisplay;

    public bool gameOver = false;
    public bool gameWin = false;

    // Use this for initialization
    void Start()
    {
        heartsToDisplay = GameObject.FindGameObjectWithTag("Player").GetComponent<Pac>().maxHealth;

        for (int i = 0; i < heartsToDisplay; i++)
        {
            hearts[i] = Instantiate(heartPrefab, heartPrefab.transform.position, heartPrefab.transform.rotation);
            hearts[i].transform.SetParent(transform, false);
            hearts[i].transform.localPosition += new Vector3((i* heart_width) + (i*5), 0, 0);
        }

        UpdateScore();
    }


    void Update()
    {
        UpdateScore();

        if (gameOver)
        {
            scoreText.GetComponent<Text>().enabled = false;
            gameOverText.GetComponent<Text>().enabled = true;
        }

        if (gameWin)
        {
            scoreText.GetComponent<Text>().enabled = false;
            gameWinText.GetComponent<Text>().enabled = true;
        }
    }

    public void UpdateScore()
    {
        gems = GameObject.FindGameObjectsWithTag("Gem");

        scoreText.GetComponent<Text>().text = "GEMS LEFT: " + gems.Length.ToString();

        if (gems.Length == 0)
        {
            gameWin = true;
        }
    }

    public void DecreaseHearts()
    {
        heartsToDisplay--;
        Destroy(hearts[heartsToDisplay]);
    }

    public void DisplayAttackPrompt()
    {
        if (GameObject.Find("Sword") != null)
        {
            attackText.GetComponent<Text>().enabled = true;
        }

    }

    public void DisplayAttackStart()
    {
        if (GameObject.Find("Sword") != null)
        {
            attackText.GetComponent<Text>().enabled = true;
        }

    }

}
