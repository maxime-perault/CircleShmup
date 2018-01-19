using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SelectMenu : ASelect
{
    enum e_button
    {
        NEWGAME = 0,
        HIGHSCORE,
        OPTIONS,
        CREDITS,
        QUITGAME
    };

    private static int id = 0;
    private GameObject music;

    void Start()
    {
        music = GameObject.Find("MusicPlayer");

        if (id == 0)
            AkSoundEngine.PostEvent("Music_Menu_Play", music);
        ++id;
    }

    public override void Select(int tmp_button)
    {
        e_button button = (e_button)tmp_button;

        AkSoundEngine.PostEvent("Main_Menu_UI_Play", WwiseCalls);

        if (button == e_button.NEWGAME)
        {
            StartCoroutine(LoadYourAsyncScene("MainGame"));
            AkSoundEngine.PostEvent("Music_Menu_Stop", music);
            AkSoundEngine.PostEvent("Music_Stop", music);
            AkSoundEngine.PostEvent("Music_Play", music);
        }
        if (button == e_button.OPTIONS)
        {
            StartCoroutine(LoadYourAsyncScene("Options"));
        }
        if (button == e_button.QUITGAME)
            Application.Quit();
    }
}
