using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SelectCredits : ASelect
{
    private GameManager manager;
    private bool initialized = false;

    private new void Start()
    {
        base.Start();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (loading == true)
            return;
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
}