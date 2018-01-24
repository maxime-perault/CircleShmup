using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectGameOver : ASelect
{
    private GameManager manager;

    private RectTransform bg;
    private float TimeToWait = 0;

    private Vector3 transVec;

    private new void Start()
    {
        base.Start();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        bg = GameObject.Find("BackgroundGO").GetComponent<RectTransform>();
        TimeToWait = Time.time + 0.5f;
        transVec = bg.anchoredPosition;
    }
	
	private void Update ()
    {
        if (Time.time > TimeToWait)
        {
            if (transVec.y == 0)
                transVec.y = 599.4f;
            else
                transVec.y = 0;
            bg.anchoredPosition = new Vector3(transVec.x, transVec.y, transVec.z);
            TimeToWait = Time.time + 0.5f;
        }

        if (manager.GetKeyUp(GameManager.e_input.ACCEPT) || Input.GetKeyDown("joystick button 0"))
        {
            AkSoundEngine.PostEvent("Main_Menu_UI_Validate", music);
            StartCoroutine(LoadYourAsyncScene("MainGame"));
        }
    }
}
