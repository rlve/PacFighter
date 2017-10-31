using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public Button bPlay;
    public Button bExit;
    public Text title;

    public GameObject Pac;
    GameObject[] enemies;

    public bool gameOver = false;
    public bool gameWin = false;

    bool startState = true;

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

        void Start () {
        if (SceneManager.GetActiveScene().name == "main")
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            Pac = GameObject.FindGameObjectWithTag("Player");
            heartsToDisplay = Pac.GetComponent<Pac>().maxHealth;

            for (int i = 0; i < heartsToDisplay; i++)
            {
                hearts[i] = Instantiate(heartPrefab, heartPrefab.transform.position, heartPrefab.transform.rotation);
                hearts[i].transform.SetParent(transform, false);
                hearts[i].transform.localPosition += new Vector3((i * heart_width) + (i * 5), 0, 0);
            }
            UpdateScore();
        } 
    }
	
	void Update () {
        if (SceneManager.GetActiveScene().name == "main")
        {
            if (startState && Input.GetKeyDown(KeyCode.Space))
            {
                foreach (var enemy in enemies)
                {
                    enemy.GetComponent<Ghost>().canMove = true;
                }
                Pac.GetComponent<Pac>().canMove = true;

                startPrompt.GetComponent<Text>().enabled = false;
                scoreText.GetComponent<Text>().enabled = true;
                startState = false;
            }

            if (gameOver || gameWin)
            {
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

                enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (var enemy in enemies)
                {
                    enemy.GetComponent<Ghost>().canMove = false;
                }
                Pac.GetComponent<Pac>().canMove = false;

                backPrompt.GetComponent<Text>().enabled = true;

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ChangeScene("menu");
                }
            }
            UpdateScore();
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

    public void PlayAnimation()
    {
        bPlay.animator.SetBool("playGame", true);
        bExit.animator.SetBool("playGame", true);
        title.GetComponent<Animator>().SetBool("playGame", true);
    }

    public void ExitAnimation()
    {
        bPlay.animator.SetBool("exitGame", true);
        bExit.animator.SetBool("exitGame", true);
        title.GetComponent<Animator>().SetBool("exitGame", true);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
