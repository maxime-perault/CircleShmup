using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_Volume : MonoBehaviour
{
    private GameObject              music;
    private SwitchButtonOptions     ButtonsClass;
    private int                     value = 50;
    private RectTransform           MainButton;
    private float[]                 rotates;
    private GameManager             manager;
    private float                   nextTime = 0;

    void setUpSound(int volume)
    {
        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = new Vector3(0, 0, rotates[volume / 10]);
        MainButton.rotation = rotation;
    }

    void FixedUpdate()
    {
        float translation = Input.GetAxisRaw("Horizontal");

        if ((ButtonsClass.getActualButton() == 1) && (Time.time > nextTime))
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

            if(MusicManager.WebGLBuildSupport)
            {
                MusicManager.SetMainVolume(value);
            }
            else
            {
                #if !UNITY_WEBGL
                    AkSoundEngine.SetRTPCValue("Music_Volume", value, music);
                #endif
            }

            music.GetComponent<MusicPlayer>().Main_Volume = value;
            nextTime = Time.time + 0.1f;
        }
    }

    private void Start()
    {
        music = GameObject.Find("MusicPlayer");
        ButtonsClass = GameObject.Find("ChangeButton").GetComponent<SwitchButtonOptions>();
        rotates = new float[11] { 90, 74, 56, 38, 20, 0, -20, -38, -56, -74, -90 };
        MainButton = GameObject.Find("Music_Button").GetComponent<RectTransform>();
        value = (int)music.GetComponent<MusicPlayer>().Main_Volume;
        setUpSound(value);
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}
