using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class SelectMenu : MonoBehaviour
{
    void Start ()
    {
    }
	
	void Update ()
    {

    }

    public void NewGame()
    {
        StartCoroutine(LoadYourAsyncScene("MainGame"));
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
