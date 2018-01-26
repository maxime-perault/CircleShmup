using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectIGMenu : ASelect
{
    private GameManager     manager;
    private GameObject      igmenu;

    public  GameObject[]    buttons;
    private bool isMoving = false;

    int actual_button = 0;

    private new void Start()
    {
        base.Start();

        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        igmenu = GameObject.Find("IGMenu");
        igmenu.SetActive(false);
    }

    void ChangeButton(int y)
    {
        if (isMoving == true)
            return;

        AkSoundEngine.PostEvent("Main_Menu_UI_Play", music);
        buttons[actual_button].GetComponent<Image>().color = new Color32(80, 80, 80, 255);

        actual_button += y;

        buttons[actual_button].GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        isMoving = true;
    }

    private void Update()
    {
        if ((manager.GetKeyDown(GameManager.e_input.PAUSE) || Input.GetKeyDown("joystick button 7")) && (manager.gameManagerState == GameManager.EGameState.GameRunning))
        {
            AkSoundEngine.PostEvent("Main_Menu_UI_Validate", music);
            manager.OnGamePaused();
            igmenu.SetActive(true);
            return;
        }

        if (manager.gameManagerState != GameManager.EGameState.GamePaused)
            return;

        float translation = Input.GetAxisRaw("Vertical");

        if ((isMoving == true) && (translation == 0))
            isMoving = false;

        if (manager.GetKeyDown(GameManager.e_input.CANCEL) || Input.GetKeyDown("joystick button 1") || Input.GetKeyDown("joystick button 7"))
        {
            AkSoundEngine.PostEvent("Main_Menu_UI_Back", music);
            igmenu.SetActive(false);
            manager.OnGameResumed();
        }

        if ((manager.GetKeyDown(GameManager.e_input.DOWN) || (translation < -0.7)) && ((actual_button + 1) < buttons.Length))
            ChangeButton(1);
        else if ((manager.GetKeyDown(GameManager.e_input.DOWN) || (translation < -0.7)) && (actual_button == (buttons.Length - 1)))
            ChangeButton(-(buttons.Length - 1));
        else if ((manager.GetKeyDown(GameManager.e_input.UP) || (translation > 0.7)) && ((actual_button - 1) >= 0))
            ChangeButton(-1);
        else if ((manager.GetKeyDown(GameManager.e_input.UP) || (translation > 0.7)) && (actual_button == 0))
            ChangeButton(buttons.Length - 1);

        if (manager.GetKeyDown(GameManager.e_input.ACCEPT) || Input.GetKeyDown("joystick button 0"))
        {
            if (actual_button == 0)
            {
                AkSoundEngine.PostEvent("Main_Menu_UI_Validate", music);
                igmenu.SetActive(false);
                manager.OnGameResumed();
            }
            else if (actual_button == 1)
            {
                AkSoundEngine.PostEvent("Friture_Stop", music);
                StartCoroutine(LoadYourAsyncScene("Menu/MainMenu"));
                manager.OnGameResumed();
                AkSoundEngine.PostEvent("Music_Menu_Stop", music);
                AkSoundEngine.PostEvent("Music_Stop", music);
                AkSoundEngine.PostEvent("Music_Menu_Play", music);
            }
        }
    }
}
