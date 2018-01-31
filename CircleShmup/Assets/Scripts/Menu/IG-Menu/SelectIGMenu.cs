using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectIGMenu : ASelect
{
    private GameManager     manager;
    private GameObject      igmenu;
    private GameObject      messageStage;

    private SelectContinue scontinue;

    public  GameObject[]    buttons;
    private bool            isMoving = false;

    int actual_button = 0;

    private new void Start()
    {
        base.Start();

        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        igmenu = GameObject.Find("IGMenu");
        igmenu.SetActive(false);
        scontinue = GameObject.Find("Continue_script").GetComponent<SelectContinue>();
    }

    void ChangeButton(int y)
    {
        if (isMoving == true)
            return;

        if (MusicManager.WebGLBuildSupport)
        {
            MusicManager.PostEvent("Main_Menu_UI_Play");
        }
        else
        {
            #if !UNITY_WEBGL
                AkSoundEngine.PostEvent("Main_Menu_UI_Play", music);
            #endif
        }

        buttons[actual_button].GetComponent<Image>().color = new Color32(80, 80, 80, 255);

        actual_button += y;

        buttons[actual_button].GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        isMoving = true;
    }

    private void Update()
    {
        if (scontinue.isActive() == true)
            return;

        if (manager.GetKeyDown(GameManager.e_input.PAUSE) && (manager.gameManagerState == GameManager.EGameState.GameRunning))
        {
            if (MusicManager.WebGLBuildSupport)
            {
                MusicManager.PostEvent("Main_Menu_UI_Validate");
            }
            else
            {
                #if !UNITY_WEBGL
                    AkSoundEngine.PostEvent("Main_Menu_UI_Validate", music);
                #endif
            }

            manager.OnGamePaused();
            igmenu.SetActive(true);
            messageStage = GameObject.Find("StageMessageText(Clone)");
            if (messageStage != null)
                messageStage.SetActive(false);
            return;
        }

        if (manager.gameManagerState != GameManager.EGameState.GamePaused)
            return;

        float translation = Input.GetAxisRaw("Vertical");

        if ((isMoving == true) && (Mathf.Round(translation) == 0))
            isMoving = false;

        if (manager.GetKeyDown(GameManager.e_input.CANCEL) || manager.GetKeyDown(GameManager.e_input.PAUSE))
        {
            if (MusicManager.WebGLBuildSupport)
            {
                MusicManager.PostEvent("Main_Menu_UI_Back");
            }
            else
            {
                #if !UNITY_WEBGL
                    AkSoundEngine.PostEvent("Main_Menu_UI_Back", music);
                #endif
            }

            igmenu.SetActive(false);
            if (messageStage != null)
                messageStage.SetActive(true);
            manager.OnGameResumed();
        }

        if (manager.GetKeyDown(GameManager.e_input.DOWN, -0.7f) && ((actual_button + 1) < buttons.Length))
            ChangeButton(1);
        else if (manager.GetKeyDown(GameManager.e_input.DOWN, -0.7f) && (actual_button == (buttons.Length - 1)))
            ChangeButton(-(buttons.Length - 1));
        else if (manager.GetKeyDown(GameManager.e_input.UP, 0.7f) && ((actual_button - 1) >= 0))
            ChangeButton(-1);
        else if (manager.GetKeyDown(GameManager.e_input.UP, 0.7f) && (actual_button == 0))
            ChangeButton(buttons.Length - 1);

        if (manager.GetKeyDown(GameManager.e_input.ACCEPT))
        {
            if (actual_button == 0)
            {
                if (MusicManager.WebGLBuildSupport)
                {
                    MusicManager.PostEvent("Main_Menu_UI_Validate");
                }
                else
                {
                    #if !UNITY_WEBGL
                        AkSoundEngine.PostEvent("Main_Menu_UI_Validate", music);
                    #endif
                }

                igmenu.SetActive(false);
                if (messageStage != null)
                    messageStage.SetActive(true);
                manager.OnGameResumed();
            }
            else if (actual_button == 1)
            {
                if (MusicManager.WebGLBuildSupport)
                {
                    MusicManager.PostEvent("Friture_Stop");
                }
                else
                {
                    #if !UNITY_WEBGL
                        AkSoundEngine.PostEvent("Friture_Stop", music);
                    #endif
                }

                StartCoroutine(LoadYourAsyncScene("Menu/MainMenu"));
                manager.OnGameResumed();

                if (MusicManager.WebGLBuildSupport)
                {
                    MusicManager.PostEvent("Music_Menu_Stop");
                    MusicManager.PostEvent("Music_Stop");
                    MusicManager.PostEvent("Music_Menu_Play");
                }
                else
                {
                    #if !UNITY_WEBGL
                        AkSoundEngine.PostEvent("Music_Menu_Stop", music);
                        AkSoundEngine.PostEvent("Music_Stop", music);
                        AkSoundEngine.PostEvent("Music_Menu_Play", music);
                    #endif
                } 
            }
        }
    }
}
