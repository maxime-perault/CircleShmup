using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwitchButtonOptions : MonoBehaviour
{
    public GameObject SelectionScript;
    public GameObject[] buttons;

    private int actual_button = 0;
    private int nb_buttons;
    private ASelect MenuClass;
    private GameManager manager;

    private GameObject music;

    private bool isMoving = false;

    void Start()
    {
        MenuClass = SelectionScript.GetComponent<ASelect>();
        nb_buttons = buttons.Length;
        music = GameObject.Find("MusicPlayer");
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void ChangeButton(int y)
    {
        buttons[actual_button].GetComponent<Image>().color = new Color32(120, 120, 120, 255);

        actual_button -= y;
        isMoving = true;

        buttons[actual_button].GetComponent<Image>().color = new Color32(220, 220, 220, 255);
    }

    void Update()
    {
        float translation = Input.GetAxisRaw("Vertical");

        /*
        ** Select Button
        */
        if (manager.GetKeyDown(GameManager.e_input.ACCEPT))
        {
            MenuClass.Select(actual_button);
        }

        /*
        ** Wait until the stick return before moving again
        */
        if ((isMoving == true) && (Mathf.Round(translation) == 0))
            isMoving = false;

        /*
        ** Move
        */
        if (isMoving == false)
        {
            if (manager.GetKeyDown(GameManager.e_input.DOWN, -0.9f) && (actual_button < (nb_buttons - 1)))
                ChangeButton(-1);
            else if (manager.GetKeyDown(GameManager.e_input.DOWN, -0.9f) && (actual_button == (nb_buttons - 1)))
                ChangeButton(nb_buttons - 1);
            else if (manager.GetKeyDown(GameManager.e_input.UP, 0.9f) && (actual_button > 0))
                ChangeButton(1);
            else if (manager.GetKeyDown(GameManager.e_input.UP, 0.9f) && (actual_button == 0))
                ChangeButton(-(nb_buttons - 1));
            else
                return;

            if (MusicManager.WebGLBuildSupport)
            {
                MusicManager.PostEvent("Main_Menu_UI_Play");
            }
            else
            {
                #if !UNITY_WEBGL
                    AkSoundEngine.PostEvent("Main_Menu_UI_Play", music);
                #endif
            }
        }
    }

    public int getActualButton()
    {
        return actual_button;
    }

    public void EnableButton(int button)
    {
        buttons[button].GetComponent<Image>().sprite
            = Resources.Load<Sprite>("Menu/options/" + buttons[button].GetComponent<Image>().sprite.name.Replace("_light", ""));
        buttons[button].GetComponent<Image>().sprite
            = Resources.Load<Sprite>("Menu/options/" + buttons[button].GetComponent<Image>().sprite.name + "_light");
    }

    public void DisableButton(int button)
    {
        buttons[button].GetComponent<Image>().sprite
            = Resources.Load<Sprite>("Menu/options/" + buttons[button].GetComponent<Image>().sprite.name.Replace("_light", ""));
    }
}
