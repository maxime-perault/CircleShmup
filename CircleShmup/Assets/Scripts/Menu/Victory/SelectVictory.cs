using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class SelectVictory : ASelect
{
    private GameManager manager;
    private GameObject enter;
    private GameObject your;

    private bool isFinal = false;
    private string nameScore;
    
    private new void Start()
    {
        base.Start();
        nameScore = "";
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enter = GameObject.Find("Enter");
        your = GameObject.Find("Your");
        enter.SetActive(false);
        Score();
    }

    private void Score()
    {
        your.transform.GetChild(0).GetComponent<Text>().text = manager.currentScore.ToString();
    }

    private void UpdateName()
    {
        string tmp = nameScore;
        string res = "";

        for (int i = tmp.Length; i < 8; i++)
            tmp += "_";
        for (int i = 0; i < 8; i++)
        {
            res += tmp[i];
            if (i != 7)
                res += " ";
        }

        enter.transform.GetChild(0).GetComponent<Text>().text = res;
    }

    private void Update()
    {
        if (manager.GetKeyUp(GameManager.e_input.ACCEPT) || Input.GetKeyDown("joystick button 0"))
        {
            if (isFinal == false)
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

                if (manager.currentScore <= manager.scoreboard[98].score)
                    StartCoroutine(LoadYourAsyncScene("Menu/MainMenu"));
                else
                {
                    your.SetActive(false);
                    enter.SetActive(true);
                    isFinal = true;
                }
            }
            else
            {
                if (nameScore != "")
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

                    manager.addScore(manager.currentScore, nameScore);
                    StartCoroutine(LoadYourAsyncScene("Menu/MainMenu"));
                }
                else
                {
                    if (MusicManager.WebGLBuildSupport)
                    {
                        MusicManager.PostEvent("Main_Menu_UI_Error");
                    }
                    else
                    {
                        #if !UNITY_WEBGL
                            AkSoundEngine.PostEvent("Main_Menu_UI_Error", music);
                        #endif
                    }
                }

            }
        }
        if (isFinal == true)
        {

            if (Input.GetKeyDown(KeyCode.Backspace) && (nameScore.Length > 0))
            {
                nameScore = nameScore.Substring(0, nameScore.Length - 1);
                UpdateName();
            }
            else if (nameScore.Length != 8)
            {
                foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(kcode)
                        && ((kcode.ToString().Length <= 1)))
                    {
                        string tmp = "";

                        tmp = Regex.Replace(kcode.ToString(), @"[^a-zA-Z0-9 ]", "");
                        nameScore += tmp;
                        if (tmp != "")
                            UpdateName();

                    }
                }
            }
        }
    }
}
