using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFX_Volume : MonoBehaviour
{
    private GameObject              music;
    private SwitchButtonOptions     ButtonsClass;
    private int                     value = 50;
    private RectTransform           FXButton;
    private float[]                 rotates;
    private GameManager             manager;
    private float                   nextTime = 0;

    void setUpSound(int volume)
    {
        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = new Vector3(0, 0, rotates[volume / 10]);
        FXButton.rotation = rotation;
    }

    void Update ()
    {
        float translation = Input.GetAxisRaw("Horizontal");

        if ((ButtonsClass.getActualButton() == 0) && (Time.time > nextTime))
        {
            if (manager.GetKey(GameManager.e_input.RIGHT, 0.8f) && ((value + 10) <= 100))
            {
                value += 10;
                setUpSound(value);
            }
            else if (manager.GetKey(GameManager.e_input.LEFT, -0.8f) && ((value - 10) >= 0))
            {
                value -= 10;
                setUpSound(value);
            }
            else
                return;
            
            if (MusicManager.WebGLBuildSupport)
            {
                MusicManager.SetSFXVolume(value);
                MusicManager.PostEvent("Main_Menu_UI_Play");
            }
            else
            {
                #if !UNITY_WEBGL
                    AkSoundEngine.SetRTPCValue("SFX_Volume", value, music);
                    AkSoundEngine.PostEvent("Main_Menu_UI_Play", music);
                #endif
            }
            music.GetComponent<MusicPlayer>().SFX_Volume = value;
            nextTime = Time.time + 0.1f;
        }
    }

    private void Start()
    {
        music = GameObject.Find("MusicPlayer");
        ButtonsClass = GameObject.Find("ChangeButton").GetComponent<SwitchButtonOptions>();
        rotates = new float[11] { 90, 74, 56, 38, 20, 0, -20, -38, -56, -74, -90 };
        FXButton = GameObject.Find("FX_Button").GetComponent<RectTransform>();
        value = (int)music.GetComponent<MusicPlayer>().SFX_Volume;
        setUpSound(value);
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}