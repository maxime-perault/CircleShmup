using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectControls : ASelect
{
    public GameObject[]     InputText;

    private GameManager     manager;

    private new void Start()
    {
        base.Start();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        InputText[0].GetComponent<Text>().text = manager.inputs[(int)GameManager.e_input.TURNLEFT];
        InputText[1].GetComponent<Text>().text = manager.inputs[(int)GameManager.e_input.TURNRIGHT];

        InputText[2].GetComponent<Text>().text = manager.inputs[(int)GameManager.e_input.ACCEPT];
        InputText[3].GetComponent<Text>().text = manager.inputs[(int)GameManager.e_input.CANCEL];
        InputText[4].GetComponent<Text>().text = manager.inputs[(int)GameManager.e_input.PAUSE];

        InputText[5].GetComponent<Text>().text = manager.inputs[(int)GameManager.e_input.UP];
        InputText[6].GetComponent<Text>().text = manager.inputs[(int)GameManager.e_input.DOWN];
        InputText[7].GetComponent<Text>().text = manager.inputs[(int)GameManager.e_input.LEFT];
        InputText[8].GetComponent<Text>().text = manager.inputs[(int)GameManager.e_input.RIGHT];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 1"))
        {
            AkSoundEngine.PostEvent("Main_Menu_UI_Validate", music);
            StartCoroutine(LoadYourAsyncScene("Menu/Options"));
        }

    }
}
