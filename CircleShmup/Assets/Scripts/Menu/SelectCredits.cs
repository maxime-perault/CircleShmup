using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SelectCredits : ASelect
{
    enum e_button
    {
        MC = 0,
        PROG,
        SD,
        GRAPH,
        GD,
        ERGO,
        BACK
    };
    
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

        AkSoundEngine.PostEvent("Main_Menu_UI_Validate", music);

        if (button == e_button.BACK)
            StartCoroutine(LoadYourAsyncScene("Menu/MainMenu"));
    }
}