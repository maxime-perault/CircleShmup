using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFX_Volume : MonoBehaviour
{
    private Slider          VolumeBar;
    private GameObject      music;
    private SwitchButton    ButtonsClass;
    private bool            isMoving = false;

    private void Start()
    {
        music = GameObject.Find("MusicPlayer");
        ButtonsClass = GameObject.Find("ChangeButton").GetComponent<SwitchButton>();
        VolumeBar = GameObject.Find("FX_Slider").GetComponent<Slider>();
        VolumeBar.value = music.GetComponent<MusicPlayer>().SFX_Volume;
    }

    void Update ()
    {
        float translation = Input.GetAxisRaw("Horizontal");

        if ((ButtonsClass.getActualButton() == 0) && (isMoving == false))
        {
            if ((translation < 0) && ((VolumeBar.value) - 10 >= 0))
                VolumeBar.value -= 10;
            else if ((translation > 0) && ((VolumeBar.value) + 10 <= 100))
                VolumeBar.value += 10;
            else
                return;
            isMoving = true;
            AkSoundEngine.SetRTPCValue("SFX_Volume", VolumeBar.value, music);
            AkSoundEngine.PostEvent("Main_Menu_UI_Play", music);
            music.GetComponent<MusicPlayer>().SFX_Volume = VolumeBar.value;
        }
        if ((isMoving == true) && (translation == 0))
            isMoving = false;
    }
}
