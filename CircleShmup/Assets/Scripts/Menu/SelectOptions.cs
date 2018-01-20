using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SelectOptions : ASelect
{
    private GameManager manager;
    private Toggle      Yaxis;
    enum e_button
    {
        SFX = 0,
        MUSIC,
        INVERT,
        BACKMENU
    };

    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Yaxis = GameObject.Find("ToggleAxis").GetComponent<Toggle>();
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

        AkSoundEngine.PostEvent("Main_Menu_UI_Validate", music);

        if (button == e_button.BACKMENU)
            StartCoroutine(LoadYourAsyncScene("Menu/MainMenu"));
        if (button == e_button.INVERT)
        {
            Yaxis.isOn = !Yaxis.isOn;
            if (Yaxis.isOn == false)
                manager.invertYaxis = 1;
            else
                manager.invertYaxis = -1;
        }
    }
}
