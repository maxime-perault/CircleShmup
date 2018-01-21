using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_Volume : MonoBehaviour
{
    private GameObject              music;
    private SwitchButtonOptions     ButtonsClass;
    private bool                    isMoving = false;
    private int                     value = 50;
    private RectTransform           MainButton;
    private float[]                 rotates;

    void setUpSound(int volume)
    {
        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = new Vector3(0, 0, rotates[volume / 10]);
        MainButton.rotation = rotation;
    }

    void Update()
    {
        float translation = Input.GetAxisRaw("Horizontal");

        if ((ButtonsClass.getActualButton() == 1) && (isMoving == false))
        {
            if ((translation > 0.8) && ((value + 10) <= 100))
            {
                value += 10;
                setUpSound(value);
            }
            else if ((translation < -0.8) && ((value - 10) >= 0))
            {
                value -= 10;
                setUpSound(value);
            }
            else
                return;

            isMoving = true;
            AkSoundEngine.SetRTPCValue("Music_Volume", value, music);
            music.GetComponent<MusicPlayer>().Main_Volume = value;

        }
        if ((isMoving == true) && (translation == 0))
            isMoving = false;
    }

    private void Start()
    {
        music = GameObject.Find("MusicPlayer");
        ButtonsClass = GameObject.Find("ChangeButton").GetComponent<SwitchButtonOptions>();
        rotates = new float[11] { 90, 74, 56, 38, 20, 0, -20, -38, -56, -74, -90 };
        MainButton = GameObject.Find("Music_Button").GetComponent<RectTransform>();
        value = (int)music.GetComponent<MusicPlayer>().Main_Volume;
        setUpSound(value);
    }
}
