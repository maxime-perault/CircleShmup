using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectControls : ASelect
{
    public GameObject[]     InputText;

    private GameManager     manager;
    private bool            isMoving = false;
    private bool            isLocked = false;
    private bool            isWaiting = false;
    private int             actual_button = 0;

    private new void Start()
    {
        base.Start();

        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        for (int i = 0; i < manager.inputs.Length; i++)
        {
            InputText[i].GetComponent<Text>().text = manager.inputs[i];
        }
    }

    public void UpdateValue(string text)
    {
        for (int i = 0; i < manager.inputs.Length; i++)
            if (manager.inputs[i] == text)
                return;
        InputText[actual_button].GetComponent<Text>().text = text;
        manager.inputs[actual_button] = text;
        isLocked = false;
        InputText[actual_button].GetComponent<Text>().color = new Color32(0, 0, 0, 255);
    }

    void ChangeButton(int y)
    {
        InputText[actual_button].transform.parent.gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        InputText[actual_button].GetComponent<Text>().color = new Color32(173, 173, 173, 255);

        actual_button += y;

        InputText[actual_button].transform.parent.gameObject.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
        InputText[actual_button].GetComponent<Text>().color = new Color32(0, 0, 0, 255);
    }

    void WaitInput()
    {
        InputText[actual_button].GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        isWaiting = true;
    }

    private void Update()
    {
        if (isWaiting == true)
        {
            if (manager.GetKeyUp(GameManager.e_input.ACCEPT))
            {
                isLocked = true;
                isWaiting = false;
            }
            else
                return;
        }
        if (isLocked == true)
        {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                    UpdateValue(kcode.ToString());
            }
            return;
        }

        if (manager.GetKeyDown(GameManager.e_input.CANCEL) || Input.GetKeyDown("joystick button 1"))
        {
            AkSoundEngine.PostEvent("Main_Menu_UI_Validate", music);
            StartCoroutine(LoadYourAsyncScene("Menu/Options"));
        }

        if (manager.GetKeyDown(GameManager.e_input.DOWN) && ((actual_button + 1) < InputText.Length))
        {
            ChangeButton(1);
            AkSoundEngine.PostEvent("Main_Menu_UI_Play", music);
        }
        else if (manager.GetKeyDown(GameManager.e_input.UP) && ((actual_button - 1) >= 0))
        {
            ChangeButton(-1);
            AkSoundEngine.PostEvent("Main_Menu_UI_Play", music);
        }

        if (manager.GetKeyDown(GameManager.e_input.ACCEPT))
        {
            AkSoundEngine.PostEvent("Main_Menu_UI_Validate", music);
            WaitInput();
        }
    }
}