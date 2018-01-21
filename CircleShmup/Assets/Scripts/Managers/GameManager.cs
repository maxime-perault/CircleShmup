using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/**
 * Main game manager, shares data between scenes
 * stores game states
 * This class is a singleton non destroyable on LoadScene
 * @class GameManager
 */

public struct ScoreBoard
{
    public string  name;
    public int     score;
};

public class GameManager : MonoBehaviour
{
    private static GameManager  SingletonRef;

    public string[] inputs;
    public ScoreBoard[] scoreboard;
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
        scoreboard = new ScoreBoard[99];

        for (int i = 0; i < 99; i++)
        {
            scoreboard[i].name = "NAMENAME";
            scoreboard[i].score = 0;
        }
        scoreboard[97].score = 97;
        scoreboard[38].score = 38;
        scoreboard[22].score = 22;
        scoreboard[21].score = 21;
        scoreboard[12].score = 12;
        scoreboard[11].score = 12;
        Array.Sort<ScoreBoard>(scoreboard, (x, y) => y.score.CompareTo(x.score));

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

    public bool GetKeyDown(GameManager.e_input input)
    {
        return Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), inputs[(int)input]));
    }

    public bool GetKeyUp(GameManager.e_input input)
    {
        return Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), inputs[(int)input]));
    }

    void Update ()
    {
		// None
	}
}
