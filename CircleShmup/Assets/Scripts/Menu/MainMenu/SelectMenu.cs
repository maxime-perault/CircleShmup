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

    private new void Start()
    {
        base.Start();
        if (id == 0)
        {
            if (MusicManager.WebGLBuildSupport)
            {
                MusicManager.PostEvent("Music_Menu_Play");
            }
            else
            {
                #if !UNITY_WEBGL
                    AkSoundEngine.PostEvent("Music_Menu_Play", music);
                #endif
            }
        }

        ++id;
    }

    public override void Select(int tmp_button)
    {
        e_button button = (e_button)tmp_button;

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

        if (button == e_button.NEWGAME)
        {
            StartCoroutine(LoadYourAsyncScene("MainGame"));
            if (MusicManager.WebGLBuildSupport)
            {
                MusicManager.PostEvent("Music_Menu_Stop");
                MusicManager.PostEvent("Music_Stop");
                MusicManager.PostEvent("Music_Play");
            }
            else
            {
                #if !UNITY_WEBGL
                    AkSoundEngine.PostEvent("Music_Menu_Stop", music);
                    AkSoundEngine.PostEvent("Music_Stop", music);
                    AkSoundEngine.PostEvent("Music_Play", music);
                #endif
            }
        }
        if (button == e_button.HIGHSCORE)
        {
            StartCoroutine(LoadYourAsyncScene("Menu/HighScore"));
        }
        if (button == e_button.OPTIONS)
        {
            StartCoroutine(LoadYourAsyncScene("Menu/Options"));
        }
        if (button == e_button.CREDITS)
        {
            StartCoroutine(LoadYourAsyncScene("Menu/Credits"));
        }
        if (button == e_button.QUITGAME)
            Application.Quit();
    }
}
