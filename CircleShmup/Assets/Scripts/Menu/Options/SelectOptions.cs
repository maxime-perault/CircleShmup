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
        if (manager.GetKeyDown(GameManager.e_input.CANCEL))
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
 
            StartCoroutine(LoadYourAsyncScene("Menu/MainMenu"));
        }
    }

    public override void Select(int tmp_button)
    {
        e_button button = (e_button)tmp_button;

        if (button == e_button.INVERT)
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

            StartCoroutine(LoadYourAsyncScene("Menu/Controls"));
        }
    }
}
