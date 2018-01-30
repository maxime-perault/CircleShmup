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
    private Text minusText;

    private int nb_life = 2;
    private int nb_sec = 5;

    private float TimeToWait = 0;

    private new void Start ()
    {
        base.Start();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        vcontinue = GameObject.Find("Continue");

        life1 = GameObject.Find("Life1");
        life2 = GameObject.Find("Life2");

        continueTime = GameObject.Find("ContinueTime").GetComponent<Text>();
        minusText = GameObject.Find("MinusContinue").GetComponent<Text>();

        vcontinue.SetActive(false);
    }

    public void GameOver()
    {
        if (nb_life > 0)
        {
            vcontinue.SetActive(true);
            Debug.Log(manager.currentScore);
            minusText.text = (manager.currentScore / 2 * -1).ToString();
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
            if (manager.GetKeyDown(GameManager.e_input.ACCEPT))
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
                --nb_life;
                life2.SetActive(false);
                GameObject.Find("Player").GetComponent<PlayerController>().hitPoint = 6;
                vcontinue.SetActive(false);
                nb_sec = 5;
                continueTime.text = nb_sec.ToString();
                ScoreManager.AddScore(manager.currentScore /= 2 * -1, this.transform.position);
                manager.OnGameResumed();
            }
            else if (manager.GetKeyDown(GameManager.e_input.CANCEL))
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
                {
                    manager.OnGameOver();
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
