using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class SelectMenu : MonoBehaviour
{
    enum e_button
    {
        NEWGAME = 0,
        HIGHSCORE,
        OPTIONS,
        CREDITS,
        QUITGAME
    };

    void Start ()
    {
    }
	
	void Update ()
    {

    }

    public void Select(int tmp_button)
    {
        e_button button = (e_button)tmp_button;

        if (button == e_button.NEWGAME)
            StartCoroutine(LoadYourAsyncScene("MainGame"));
        if (button == e_button.QUITGAME)
            Application.Quit();
    }

    /*
    ** Load the scene and wait with a yield until its done.
    */
    IEnumerator LoadYourAsyncScene(string name)
    {
        string path = "Scenes/"; path += name;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(path);
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
