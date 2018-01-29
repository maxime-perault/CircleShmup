using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class IntroManager : ASelect
{
    public GameObject first;
    public GameObject second;
    public GameObject inter;

    private GameManager manager;

    private int step = 0;
    private float TimeToWait = 0;

    private new void Start()
    {
        base.Start();

        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        ResetAlpha(first.GetComponent<Image>());
        ResetAlpha(first.transform.GetChild(0).GetComponent<Image>());
        ResetAlpha(first.transform.GetChild(1).GetComponent<Text>());

        ResetAlpha(second.GetComponent<Image>());
        ResetAlpha(second.transform.GetChild(0).GetComponent<Image>());
    }

    void DecreaseAlpha(Image image)
    {
        Color32 tmpColor;

        tmpColor = image.color;
        tmpColor.a -= 5;
        image.color = tmpColor;
    }

    void DecreaseAlpha(Text image)
    {
        Color32 tmpColor;

        tmpColor = image.color;
        tmpColor.a -= 5;
        image.color = tmpColor;
    }

    void IncreaseAlpha(Image image)
    {
        Color32 tmpColor;

        tmpColor = image.color;
        tmpColor.a += 5;
        image.color = tmpColor;
    }

    void IncreaseAlpha(Text image)
    {
        Color32 tmpColor;

        tmpColor = image.color;
        tmpColor.a += 5;
        image.color = tmpColor;
    }

    void ResetAlpha(Text image)
    {
        Color32 tmpColor;

        tmpColor = image.color;
        tmpColor.a = 0;
        image.color = tmpColor;
    }

    void ResetAlpha(Image image)
    {
        Color32 tmpColor;

        tmpColor = image.color;
        tmpColor.a = 0;
        image.color = tmpColor;
    }

    void FadeOutFirst()
    {
        DecreaseAlpha(first.GetComponent<Image>());
        DecreaseAlpha(first.transform.GetChild(0).GetComponent<Image>());
        DecreaseAlpha(first.transform.GetChild(1).GetComponent<Text>());
        if (first.GetComponent<Image>().color.a == 0)
            ++step;
    }

    void FadeInFirst()
    {
        IncreaseAlpha(first.GetComponent<Image>());
        IncreaseAlpha(first.transform.GetChild(0).GetComponent<Image>());
        IncreaseAlpha(first.transform.GetChild(1).GetComponent<Text>());
        if (first.GetComponent<Image>().color.a == 1)
            ++step;
    }

    void FadeOutSecond()
    {
        DecreaseAlpha(second.GetComponent<Image>());
        DecreaseAlpha(second.transform.GetChild(0).GetComponent<Image>());
        if (second.GetComponent<Image>().color.a == 0)
            ++step;
    }

    void FadeInSecond()
    {
        IncreaseAlpha(second.GetComponent<Image>());
        IncreaseAlpha(second.transform.GetChild(0).GetComponent<Image>());
        if (second.GetComponent<Image>().color.a == 1)
            ++step;
    }

    private void Update()
    {
        if (step == 0)
        {
            FadeInFirst();
            TimeToWait = Time.time + 1f;
        }
        else if ((step == 1) && (Time.time > TimeToWait))
        {
            FadeOutFirst();
        }
        else if (step == 2)
        {
            inter.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            ++step;
        }
        else if (step == 3)
        {
            FadeInSecond();
            TimeToWait = Time.time + 1f;
        }
        else if ((step == 4) && (Time.time > TimeToWait))
        {
            FadeOutSecond();
        }
        else if (step == 5)
            StartCoroutine(LoadYourAsyncScene("Menu/MainMenu"));

        if (manager.GetKeyUp(GameManager.e_input.ACCEPT) || Input.GetKeyDown("joystick button 0"))
        {
            if ((step == 0) || (step == 1))
            {
                if (MusicManager.WebGLBuildSupport)
                {
                    MusicManager.PostEvent("Main_Menu_UI_Validate");
                }
                else
                {
                    AkSoundEngine.PostEvent("Main_Menu_UI_Validate", music);
                }
 
                inter.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
                ResetAlpha(first.GetComponent<Image>());
                ResetAlpha(first.transform.GetChild(0).GetComponent<Image>());
                ResetAlpha(first.transform.GetChild(1).GetComponent<Text>());
                step = 3;
            }
            else if ((step == 3) || (step == 4))
            {
                if (MusicManager.WebGLBuildSupport)
                {
                    MusicManager.PostEvent("Main_Menu_UI_Validate");
                }
                else
                {
                    AkSoundEngine.PostEvent("Main_Menu_UI_Validate", music);
                }
                
                StartCoroutine(LoadYourAsyncScene("Menu/MainMenu"));
            }
        }
    }
}
