using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Main game manager, shares data between scenes
 * stores game states
 * This class is a singleton non destroyable on LoadScene
 * @class GameManager
 */
public class GameManager : MonoBehaviour
{
    private static GameManager  SingletonRef;

    public string[] inputs;
    public int      invertYaxis = 1;

    public enum e_input
    {
        TURNLEFT = 0,
        TURNRIGHT,
        ACCEPT,
        CANCEL,
        PAUSE,
        UP,
        DOWN,
        LEFT,
        RIGHT
    };

    void Awake()
    {
        if (SingletonRef == null)
        {
            SingletonRef = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    void Start ()
    {
        inputs = new string[System.Enum.GetNames(typeof(e_input)).Length];

        inputs[(int)e_input.TURNLEFT] = "Mouse0";
        inputs[(int)e_input.TURNRIGHT] = "Mouse1";

        inputs[(int)e_input.ACCEPT] = "Space";
        inputs[(int)e_input.CANCEL] = "Escape";
        inputs[(int)e_input.PAUSE] = "Escape";

        inputs[(int)e_input.UP] = "UpArrow";
        inputs[(int)e_input.DOWN] = "DownArrow";
        inputs[(int)e_input.LEFT] = "LeftArrow";
        inputs[(int)e_input.RIGHT] = "RightArrow";
    }
	
	void Update ()
    {
		// None
	}
}
