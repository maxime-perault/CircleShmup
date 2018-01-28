using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectContinue : ASelect
{
    private GameManager manager;

    private GameObject life1, life2;
    private GameObject vcontinue;
    private Text continueTime;

    private int nb_life = 2;
    private int nb_sec = 5;

    private float TimeToWait = 0;

    void Start ()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        vcontinue = GameObject.Find("Continue");

        life1 = GameObject.Find("Life1");
        life2 = GameObject.Find("Life2");

        continueTime = GameObject.Find("ContinueTime").GetComponent<Text>();

        vcontinue.SetActive(false);
    }

    public void GameOver()
    {
        if (nb_life > 0)
        {
            vcontinue.SetActive(true);
            TimeToWait = Time.realtimeSinceStartup + 1f;
            manager.OnGamePaused();
        }
        else
            manager.OnGameOver();
    }

    public bool isActive()
    {
        return vcontinue.activeSelf;
    }

    void Update ()
    {
        if (vcontinue.activeSelf == true)
        {
            if (manager.GetKeyUp(GameManager.e_input.ACCEPT) || Input.GetKeyDown("joystick button 0"))
            {
                AkSoundEngine.PostEvent("Main_Menu_UI_Validate", music);
                --nb_life;
                life2.SetActive(false);
                GameObject.Find("Player").GetComponent<PlayerController>().hitPoint = 6;
                vcontinue.SetActive(false);
                nb_sec = 5;
                continueTime.text = nb_sec.ToString();
                manager.OnGameResumed();
            }
            else if (manager.GetKeyDown(GameManager.e_input.CANCEL) || Input.GetKeyDown("joystick button 1"))
            {
                manager.OnGameOver();
            }
            if (Time.realtimeSinceStartup > TimeToWait)
            {
                if (nb_sec >= 1)
                {
                    --nb_sec;
                    continueTime.text = nb_sec.ToString();
                    TimeToWait = Time.realtimeSinceStartup + 1f;
                }
                else
                    manager.OnGameOver();
            }
        }
    }
}
