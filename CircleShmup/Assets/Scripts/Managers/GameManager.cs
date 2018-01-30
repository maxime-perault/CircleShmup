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

    public KeyCode[]    inputs;
    public KeyCode[]    controllerInputs;
    public ScoreBoard[] scoreboard;
    public int          invertYaxis = 1;

    /*
     * Called once when the object is loaded
     */
    void Awake()
    {
        Cursor.visible = false;
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
                for (int i = 0; i < scores.Length; ++i)
                {
                    scoreboard[i].score = scores[i].Value;
                    scoreboard[i].name = scores[i].GuestName;
                }
            }
        }, 0, 99);
        /*
        ** Keyboard default inputs
        */
        inputs = new KeyCode[System.Enum.GetNames(typeof(e_input)).Length];

        inputs[(int)e_input.TURNLEFT] = KeyCode.Q;
        inputs[(int)e_input.TURNRIGHT] = KeyCode.D;

        inputs[(int)e_input.ACCEPT] = KeyCode.Space;
        inputs[(int)e_input.CANCEL] = KeyCode.Escape;
        inputs[(int)e_input.PAUSE] = KeyCode.Escape;

        inputs[(int)e_input.UP] = KeyCode.UpArrow;
        inputs[(int)e_input.DOWN] = KeyCode.DownArrow;
        inputs[(int)e_input.LEFT] = KeyCode.LeftArrow;
        inputs[(int)e_input.RIGHT] = KeyCode.RightArrow;

        /*
        ** Controller default inputs
        */
        controllerInputs = new KeyCode[System.Enum.GetNames(typeof(e_input)).Length];

        controllerInputs[(int)e_input.TURNLEFT] = KeyCode.JoystickButton4;
        controllerInputs[(int)e_input.TURNRIGHT] = KeyCode.JoystickButton5;

        controllerInputs[(int)e_input.ACCEPT] = KeyCode.JoystickButton0;
        controllerInputs[(int)e_input.CANCEL] = KeyCode.JoystickButton1;
        controllerInputs[(int)e_input.PAUSE] = KeyCode.JoystickButton7;
        
        //those inputs have to be the same as keyboard for the OR bitwise operator in GetKeyDown
        controllerInputs[(int)e_input.UP] = KeyCode.UpArrow;
        controllerInputs[(int)e_input.DOWN] = KeyCode.DownArrow;
        controllerInputs[(int)e_input.LEFT] = KeyCode.LeftArrow;
        controllerInputs[(int)e_input.RIGHT] = KeyCode.RightArrow;

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

        if(MusicManager.WebGLBuildSupport)
        {
            MusicManager.PostEvent("Friture");
        }
        else
        {
            #if !UNITY_WEBGL
                AkSoundEngine.PostEvent("Friture", musicPlayer);
            #endif
        }
        
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
        gameManagerState = EGameState.GamePaused;
    }

    /**
     * TODO
      */
    public void OnGameResumed()
    {
        Time.timeScale = 1.0f;
        gameManagerState = EGameState.GameRunning;
    }

    /**
     * TODO
     */
    public void OnGameOver()
    {
        if (MusicManager.WebGLBuildSupport)
        {
            MusicManager.PostEvent("Friture_Stop");
            MusicManager.PostEvent("End_Fail");
        }
        else
        {
            #if !UNITY_WEBGL
                AkSoundEngine.PostEvent("Friture_Stop", musicPlayer);
                AkSoundEngine.PostEvent("End_Fail", musicPlayer);
            #endif
        }

        gameOverController.GameOver();
        Time.timeScale = 1.0f;
    }

    /**
     * TODO
     */
    public void OnGameWin()
    {
        if (MusicManager.WebGLBuildSupport)
        {
            MusicManager.PostEvent("Friture_Stop");
            MusicManager.PostEvent("End_Victory");
        }
        else
        {
            #if !UNITY_WEBGL
                AkSoundEngine.PostEvent("Friture_Stop", musicPlayer);
                AkSoundEngine.PostEvent("End_Victory", musicPlayer);
            #endif
        }

        gameWinController.GameWin();
        Time.timeScale = 1.0f;
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
    
    public bool GetKeyDown(GameManager.e_input input, float max = 0)
    {
        switch (input)
        {
            case e_input.UP:
            {
                if (Input.GetAxisRaw("Vertical") > max)
                    return true;
                break;
            }
            case e_input.DOWN:
            {
                if (Input.GetAxisRaw("Vertical") < max)
                    return true;
                break;
            }
            case e_input.LEFT:
            {
                if (Input.GetAxisRaw("Horizontal") < max)
                    return true;
                break;
            }
            case e_input.RIGHT:
            {
                if (Input.GetAxisRaw("Horizontal") > max)
                    return true;
                break;
            }
            default:
                break;
        }
        return (Input.GetKeyDown(inputs[(int)input]) | Input.GetKeyDown(controllerInputs[(int)input]));
    }

    public bool GetKey(GameManager.e_input input, float max = 0)
    {
        switch (input)
        {
            case e_input.UP:
                {
                    if (Input.GetAxisRaw("Vertical") > max)
                        return true;
                    break;
                }
            case e_input.DOWN:
                {
                    if (Input.GetAxisRaw("Vertical") < max)
                        return true;
                    break;
                }
            case e_input.LEFT:
                {
                    if (Input.GetAxisRaw("Horizontal") < max)
                        return true;
                    break;
                }
            case e_input.RIGHT:
                {
                    if (Input.GetAxisRaw("Horizontal") > max)
                        return true;
                    break;
                }
            default:
                break;
        }
        return (Input.GetKey(inputs[(int)input]) | Input.GetKey(controllerInputs[(int)input]));
    }

    public bool GetKeyUp(GameManager.e_input input, float max = 0)
    {
        return (Input.GetKeyUp(inputs[(int)input]) | Input.GetKeyUp(controllerInputs[(int)input]));
    }
}
