﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour
{
    private float translation = 0;
    private Vector2 pos;
    private int actual_button = 0;
    private bool isMoving = false;
    private SelectMenu MenuClass;


    public int nb_buttons = 4;
    public GameObject SelectionScript;
    public GameObject[] buttons;

    void Start ()
    {
        pos.x = GetComponent<RectTransform>().localPosition.x;
        pos.y = GetComponent<RectTransform>().localPosition.y;
        MenuClass = SelectionScript.GetComponent<SelectMenu>();
    }
	
    void ChangeButton(int y)
    {
        buttons[actual_button].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        buttons[actual_button].GetComponentInChildren<Text>().color = new Color32(0 ,0, 0, 255);
        actual_button += -y;
        pos.y += (y * 100);
        GetComponent<RectTransform>().localPosition = new Vector3(pos.x, pos.y, 0);
        isMoving = true;
        buttons[actual_button].GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        buttons[actual_button].GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
    }

	void Update ()
    {
        translation = Input.GetAxisRaw("Vertical");

        /*
        ** SpaceShip shots
        */
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey("joystick button 0"))
            && (actual_button == 0))
        {
            MenuClass.NewGame();
        }

        /*
        ** Wait until the stick return before moving again
        */
        if ((isMoving == true) && (translation == 0))
            isMoving = false;

        /*
        ** Move the ship across the buttons
        */
        if (isMoving == false)
        {
            if ((translation < 0) && (actual_button < (nb_buttons - 1)))
            {
                ChangeButton(-1);
                
            }
            else if ((translation > 0) && (actual_button > 0))
            {
                ChangeButton(1);
            }
        }
    }
}
