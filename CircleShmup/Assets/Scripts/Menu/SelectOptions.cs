using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SelectOptions : ASelect
{
    enum e_button
    {
        SFX = 0,
        MUSIC,
        AZERTY,
        INVERT,
        BACKMENU
    };

    private GameObject music;

    private void Start()
    {
        music = GameObject.Find("MusicPlayer");
    }

    public override void Select(int tmp_button)
    {
        e_button button = (e_button)tmp_button;

        AkSoundEngine.PostEvent("Main_Menu_UI_Validate", music);

        if (button == e_button.BACKMENU)
            StartCoroutine(LoadYourAsyncScene("MainMenu"));
    }
}
