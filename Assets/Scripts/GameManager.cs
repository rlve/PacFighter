using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public Button bPlay;
    public Button bExit;
    public Text title;

    GameObject Pac;
    GameObject[] enemies;
    public UI_handler ui_handler;


    bool startState = true;
    bool endState = false;

    void Start () {
        if (SceneManager.GetActiveScene().name == "main")
        {
            ui_handler = GameObject.Find("Canvas").GetComponent<UI_handler>();
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            Pac = GameObject.FindGameObjectWithTag("Player");
        }
        
    }
	

	void Update () {
       
        if (SceneManager.GetActiveScene().name == "main" && Input.GetKeyDown(KeyCode.Space) & startState)
        {
            
            foreach (var enemy in enemies)
            {
                enemy.GetComponent<Ghost>().idle = false;
            }
            Pac.GetComponent<Pac>().canMove = true;

            ui_handler.startPrompt.GetComponent<Text>().enabled = false;
            ui_handler.scoreText.GetComponent<Text>().enabled = true;
            startState = false;
        }

        if (SceneManager.GetActiveScene().name == "main" && (ui_handler.gameOver || ui_handler.gameWin))
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                enemy.GetComponent<Ghost>().idle = true;
            }
            Pac.GetComponent<Pac>().canMove = false;

            ui_handler.backPrompt.GetComponent<Text>().enabled = true;
        }

        if (SceneManager.GetActiveScene().name == "main" && Input.GetKeyDown(KeyCode.Escape) & (ui_handler.gameOver || ui_handler.gameWin))
        {
            ChangeScene("menu");
        }


    }


    public void PlayGame()
    {
        bPlay.animator.SetBool("playGame", true);
        bExit.animator.SetBool("playGame", true);
        title.GetComponent<Animator>().SetBool("playGame", true);
    }

    void PlayState()
    {
        ChangeScene("main");
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitAnimation()
    {
        bPlay.animator.SetBool("exitGame", true);
        bExit.animator.SetBool("exitGame", true);
        title.GetComponent<Animator>().SetBool("exitGame", true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}
