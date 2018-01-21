using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SelectOptions : ASelect
{
    private GameManager         manager;
    private SwitchButtonOptions ButtonsClass;

    enum e_button
    {
        SFX = 0,
        MUSIC,
        INVERT,
        CONTROLS
    };

    private new void Start()
    {
        base.Start();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        ButtonsClass = GameObject.Find("ChangeButton").GetComponent<SwitchButtonOptions>();

        if (manager.invertYaxis == 1)
            ButtonsClass.DisableButton((int)e_button.INVERT);
        else
            ButtonsClass.EnableButton((int)e_button.INVERT);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 1"))
        {
            AkSoundEngine.PostEvent("Main_Menu_UI_Validate", music);
            StartCoroutine(LoadYourAsyncScene("Menu/MainMenu"));
        }
    }

    public override void Select(int tmp_button)
    {
        e_button button = (e_button)tmp_button;

        if (button == e_button.INVERT)
        {
            AkSoundEngine.PostEvent("Main_Menu_UI_Validate", music);
            if (manager.invertYaxis == 1)
            {
                ButtonsClass.EnableButton((int)e_button.INVERT);
                manager.invertYaxis = -1;
            }
            else
            {
                ButtonsClass.DisableButton((int)e_button.INVERT);
                manager.invertYaxis = 1;
            }
        }
        if (button == e_button.CONTROLS)
        {
            AkSoundEngine.PostEvent("Main_Menu_UI_Validate", music);
            StartCoroutine(LoadYourAsyncScene("Menu/Controls"));
        }
    }
}
