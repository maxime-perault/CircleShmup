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

    public bool              isMainSceneLoaded;
    public int               currentScore;

    private GameObject       musicPlayer;
    private StageManager     stageManager;
    private PlayerController playerController;
    public EGameState        gameManagerState;

    private static GameManager        SingletonRef;
    private        GameOverController gameOverController;
    private        GameWinController  gameWinController;

    public string[]     inputs;
    public ScoreBoard[] scoreboard;
    public int          invertYaxis = 1;

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

    /*
     * Called when the object is instanciated
     */
    void Start()
    {
        currentScore      = 0;
        stageManager      = null;
        isMainSceneLoaded = false;
        gameManagerState  = EGameState.GameNone;

        inputs = new string[System.Enum.GetNames(typeof(e_input)).Length];
        scoreboard = new ScoreBoard[99];

        for (int i = 0; i < 99; i++)
        {
            scoreboard[i].name = "NAMENAME";
            scoreboard[i].score = 0;
        }
        
        //Get the highscoreboard from GameJolt
        GameJolt.API.Scores.Get(scores => {
            if (scores != null)
            {
                Debug.Log(scores.Length);
                for (int i = 0; i < scores.Length; ++i)
                {
                    scoreboard[i].score = scores[i].Value;
                    scoreboard[i].name = scores[i].GuestName;
                }
            }
        }, 0, 99);
        inputs[(int)e_input.TURNLEFT] = "Mouse0";
        inputs[(int)e_input.TURNRIGHT] = "Mouse1";

        inputs[(int)e_input.ACCEPT] = "Space";
        inputs[(int)e_input.CANCEL] = "Escape";
        inputs[(int)e_input.PAUSE] = "Escape";

        inputs[(int)e_input.UP] = "UpArrow";
        inputs[(int)e_input.DOWN] = "DownArrow";
        inputs[(int)e_input.LEFT] = "LeftArrow";
        inputs[(int)e_input.RIGHT] = "RightArrow";

        gameOverController = GetComponent<GameOverController>();
        gameWinController  = GetComponent<GameWinController>();
    }

    public void addScore(int score, string name, int tableID = 319146)
    {

        if (score > scoreboard[98].score)
        {
            scoreboard[98].name = name;
            scoreboard[98].score = score;
        }
        GameJolt.API.Scores.Add(score, score.ToString(), name, 0, "", (bool success) => {
            Debug.Log(string.Format("Score Add {0}.", success ? "Successful" : "Failed"));
        });
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
            OnGameExit();
            isMainSceneLoaded = false;
            gameManagerState = EGameState.GameNone;
        }
    }

    /**
     * TODO
     */
    public void OnGameEnter()
    {
        musicPlayer      = GameObject.Find("MusicPlayer");
        stageManager     = GameObject.Find("StageManager").GetComponent<StageManager>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (stageManager == null)
        {
            Debug.LogError("Fatal error, the stage manager is not instanciated !");
        }

        currentScore      = 0;
        isMainSceneLoaded = true;
        gameManagerState  = EGameState.GameRunning;

        AkSoundEngine.PostEvent("Friture",      musicPlayer);
    }

    /**
     * TODO
     */
    public void OnGameExit()
    {
        musicPlayer      = null;
        stageManager     = null;
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
    public void OnGameOver()
    {
        AkSoundEngine.PostEvent("Friture_Stop", musicPlayer);
        AkSoundEngine.PostEvent("End_Fail",     musicPlayer);
        gameOverController.GameOver();
    }

    /**
     * TODO
     */
    public void OnGameWin()
    {
        AkSoundEngine.PostEvent("Friture_Stop", musicPlayer);
        AkSoundEngine.PostEvent("End_Victory",  musicPlayer);
        gameWinController.GameWin();
    }

    /**
     * TODO
     */
    public void Update()
    {
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
