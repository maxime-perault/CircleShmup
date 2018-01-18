﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{
    public GameObject   SelectionScript;
    public GameObject[] buttons;
    
    private int         actual_button = 0;
    private int         nb_buttons;
    private SelectMenu  MenuClass;
    
    private bool        isMoving = false;

    void Start ()
    {
        MenuClass = SelectionScript.GetComponent<SelectMenu>();
        nb_buttons = buttons.Length;
    }
	
    void ChangeButton(int y)
    {
        buttons[actual_button].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        buttons[actual_button].GetComponentInChildren<Text>().color = new Color32(0 ,0, 0, 255);
        actual_button -= y;
        isMoving = true;
        buttons[actual_button].GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        buttons[actual_button].GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
    }

	void Update ()
    {
        float translation = Input.GetAxisRaw("Vertical");

        /*
        ** Select
        */
        if (Input.GetKey(KeyCode.Space) || Input.GetKey("joystick button 0"))
        {
            MenuClass.Select(actual_button);
        } 

        /*
        ** Wait until the stick return before moving again
        */
        if ((isMoving == true) && (translation == 0))
            isMoving = false;

        /*
        ** Move
        */
        if (isMoving == false)
        {
            if ((translation < 0) && (actual_button < (nb_buttons - 1)))
                ChangeButton(-1);
            else if ((translation < 0) && (actual_button == (nb_buttons - 1)))
                ChangeButton(nb_buttons - 1);
            else if ((translation > 0) && (actual_button > 0))
                ChangeButton(1);
            else if ((translation > 0) && (actual_button == 0))
                ChangeButton(-(nb_buttons - 1));
        }
    }
}
