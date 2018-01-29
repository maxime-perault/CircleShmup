﻿using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SelectCredits : ASelect
{
    private GameManager manager;

    private new void Start()
    {
        base.Start();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (manager.GetKeyDown(GameManager.e_input.CANCEL) || Input.GetKeyDown("joystick button 1"))
        {
            if (MusicManager.WebGLBuildSupport)
            {
                MusicManager.PostEvent("Main_Menu_UI_Back");
            }
            else
            {
                AkSoundEngine.PostEvent("Main_Menu_UI_Back", music);
            }

            StartCoroutine(LoadYourAsyncScene("Menu/MainMenu"));
        }

    }
}