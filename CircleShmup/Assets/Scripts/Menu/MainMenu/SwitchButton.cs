using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class SwitchButton : MonoBehaviour
{
    public GameObject   SelectionScript;
    public GameObject[] buttons;
    
    private int         actual_button = 0;
    private int         nb_buttons;
    private ASelect     MenuClass;
    private GameManager manager;

    private GameObject      music;
    private RectTransform   masterButton;
    private float[]         rotates;
    
    private bool        isMoving = false;

    void Start ()
    {
        MenuClass = SelectionScript.GetComponent<ASelect>();
        nb_buttons = buttons.Length;
        music = GameObject.Find("MusicPlayer");
        masterButton = GameObject.Find("MasterButton").GetComponent<RectTransform>();
        rotates = new float[5] {45, 12, -27, -61, -90};
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
    void ChangeButton(int y)
    {
        buttons[actual_button].GetComponent<Image>().sprite
            = Resources.Load<Sprite>("Menu/menu/" + buttons[actual_button].GetComponent<Image>().sprite.name.Replace("_light", ""));

        actual_button -= y;
        isMoving = true;
        
        buttons[actual_button].GetComponent<Image>().sprite
            = Resources.Load<Sprite>("Menu/menu/" + buttons[actual_button].GetComponent<Image>().sprite.name + "_light");

        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = new Vector3(0, 0, rotates[actual_button]);
        masterButton.rotation = rotation;
    }

	void Update ()
    {
        float translation = Input.GetAxisRaw("Vertical");

        if (MenuClass.loading == true)
            return;
        /*
        ** Select
        */
        if (manager.GetKeyDown(GameManager.e_input.ACCEPT))
        {
            MenuClass.Select(actual_button);
        }

        /*
        ** Wait until the stick return before moving again
        */
        if ((isMoving == true) && (Mathf.Round(translation) == 0))
        {
            isMoving = false;
        }

        /*
        ** Move
        */
        if (isMoving == false)
        {
            if (manager.GetKeyDown(GameManager.e_input.DOWN, -0.8f) && (actual_button < (nb_buttons - 1)))
                ChangeButton(-1);
            else if (manager.GetKeyDown(GameManager.e_input.DOWN, -0.8f) && (actual_button == (nb_buttons - 1)))
                ChangeButton(nb_buttons - 1);
            else if (manager.GetKeyDown(GameManager.e_input.UP, 0.8f) && (actual_button > 0))
                ChangeButton(1);
            else if (manager.GetKeyDown(GameManager.e_input.UP, 0.8f) && (actual_button == 0))
                ChangeButton(-(nb_buttons - 1));
            else
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
        }
    }

    public int getActualButton()
    {
        return actual_button;
    }
}
