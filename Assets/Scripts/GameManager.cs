using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public Button bPlay;
    public Button bExit;
    public Text title;
    

    void Start () {
    }
	

	void Update () {
		
	}


    public void PlayGame()
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

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
