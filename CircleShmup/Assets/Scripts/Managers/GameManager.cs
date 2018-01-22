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

    public string[]     inputs;
    public ScoreBoard[] scoreboard;
    public int          invertYaxis = 1;

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
        
        Array.Sort<ScoreBoard>(scoreboard, (x, y) => y.score.CompareTo(x.score));

        addScore(97, "TESTTEST");
        addScore(38, "TESTTEST");
        addScore(22, "TESTTEST");
        addScore(12, "TESTTEST");
        addScore(21, "TESTTEST");
        addScore(12, "TESTTEST");


        inputs[(int)e_input.TURNLEFT] = "LeftControl";
        inputs[(int)e_input.TURNRIGHT] = "Space";

        inputs[(int)e_input.ACCEPT] = "Space";
        inputs[(int)e_input.CANCEL] = "Escape";
        inputs[(int)e_input.PAUSE] = "Escape";

        inputs[(int)e_input.UP] = "Z";
        inputs[(int)e_input.DOWN] = "S";
        inputs[(int)e_input.LEFT] = "Q";
        inputs[(int)e_input.RIGHT] = "D";
    }

    public void addScore(int score, string name)
    {
        
        if (score > scoreboard[98].score)
        {
            scoreboard[98].name = name;
            scoreboard[98].score = score;
        }
        Array.Sort<ScoreBoard>(scoreboard, (x, y) => y.score.CompareTo(x.score));
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
