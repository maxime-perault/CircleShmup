using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

/**
 * Helper struct for scores
 */
public struct ScoreBoard
{
    public string  name;
    public int     score;
};

namespace GameJolt.UI.Controllers
{
    public class APIScore : MonoBehaviour
    {
        public void GetAllScores(int tableID = 319146)
        {
            API.Scores.Get(scores => {
                if (scores != null)
                {
                    for (int i = 0; i < scores.Length; ++i)
                    {
                        Debug.Log(scores[i]);
                    }
                }
            }, tableID, 99);
        }
    }
}

/**
 * Main game manager, shares data between scenes
 * stores game states
 * This class is a singleton non destroyable on LoadScene
 * @class GameManager
 */
public class GameManager : MonoBehaviour
{
    /**
        * Enum to adress inputs name with 
        * natural language
        */
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

    /**
        * Enum to store the current game state 
        */
    public enum EGameState
    {
        GameNone,
        GamePaused,
        GameRunning
    }

    public bool isMainSceneLoaded;
    private StageManager stageManager;
    private PlayerController playerController;
    private GameJolt.UI.Controllers.APIScore api;
    public EGameState gameManagerState;

    private static GameManager SingletonRef;

    public string[] inputs;
    public ScoreBoard[] scoreboard;
    public int invertYaxis = 1;

    /*
     * Called once when the object is loaded
     */
    void Awake()
    {
        if (SingletonRef == null)
        {
            SingletonRef = this;

            // Make sure that the object is not destroyed
            // when loading scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }

        // Subscribes to the scene managment events
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /**
        * Called when the object is instanciated
        */
    void Start()
    {
        stageManager = null;
        isMainSceneLoaded = false;
        gameManagerState = EGameState.GameNone;

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

        //api.GetAllScores();

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


    public void addScore(int score, string name, int tableID = 319146)
    {

        if (score > scoreboard[98].score)
        {
            scoreboard[98].name = name;
            scoreboard[98].score = score;
        }
        Array.Sort<ScoreBoard>(scoreboard, (x, y) => y.score.CompareTo(x.score));
    }


    /**
        * Called when a scene is loaded (Unity Callback)
        */
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainGame")
        {
            OnGameEnter();
        }
        else
        {
            isMainSceneLoaded = false;
            gameManagerState = EGameState.GameNone;
        }
    }

    /**
        * TODO
        */
    public void OnGameEnter()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (stageManager == null)
        {
            Debug.LogError("Fatal error, the stage manager is not instanciated !");
        }

        isMainSceneLoaded = true;
        gameManagerState = EGameState.GameRunning;
    }

    /**
        * TODO
        */
    public void OnGameExit()
    {
        stageManager = null;
        playerController = null;
    }

    /**
        * TODO
        */
    public void OnGamePaused()
    {
        Time.timeScale = 0.0f;
        //stageManager.OnGamePaused();
        //playerController.OnGamePaused();

        // TODO Trigger interface
        gameManagerState = EGameState.GamePaused;
    }

    /**
        * TODO
        */
    public void OnGameResumed()
    {
        Time.timeScale = 1.0f;
        //stageManager.OnGameResumed();
        //playerController.OnGameResumed();

        // TODO Remove interface
        gameManagerState = EGameState.GameRunning;
    }

    /**
        * TODO
        */
    public void Update()
    {
        if (isMainSceneLoaded)
        {
            if (GetKeyDown(e_input.CANCEL) && gameManagerState == EGameState.GameRunning)
            {
                // Pause request
                OnGamePaused();
            }
            else if (GetKeyDown(e_input.CANCEL) && gameManagerState == EGameState.GamePaused)
            {
                // Resume request
                OnGameResumed();
            }
        }
    }

    /**
        * Called when the game has to be paused 
        * (back to desktop etc. - This is a Unity Callback) 
        */
    public void OnApplicationPause(bool pause)
    {
        if (isMainSceneLoaded)
        {
            if (pause)
            {
                OnGamePaused();
            }
            else
            {
                OnGameResumed();
            }
        }
    }

    /**
        * TODO
        */
    public bool GetKeyDown(GameManager.e_input input)
    {
        return Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), inputs[(int)input]));
    }

    /**
        * TODO
        */
    public bool GetKeyUp(GameManager.e_input input)
    {
        return Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), inputs[(int)input]));
    }
}
